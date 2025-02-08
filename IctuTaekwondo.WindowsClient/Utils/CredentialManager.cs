using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IctuTaekwondo.WindowsClient.Utils
{
    public class CredentialManager
    {
        public const string ApplicationName = "IctuTaekwondoApp";

        public static void SaveCredentials(string applicationName, string userName, string password)
        {
            var credential = new CREDENTIAL
            {
                TargetName = Marshal.StringToCoTaskMemUni(applicationName),
                UserName = Marshal.StringToCoTaskMemUni(userName),
                CredentialBlob = Marshal.StringToCoTaskMemUni(password),
                CredentialBlobSize = (uint)Encoding.Unicode.GetBytes(password).Length,
                AttributeCount = 0,
                Attributes = IntPtr.Zero,
                Comment = IntPtr.Zero,
                TargetAlias = IntPtr.Zero,
                Type = CredentialType.Generic,
                Persist = (uint)CredentialPersist.LocalMachine
            };

            bool result = CredWrite(ref credential, 0);
            Marshal.FreeCoTaskMem(credential.CredentialBlob);

            if (!result)
            {
                throw new Exception("Failed to save credentials.");
            }
        }

        public static string RetrieveCredentials(string applicationName, out string userName)
        {
            IntPtr credentialPtr;
            bool result = CredRead(applicationName, CredentialType.Generic, 0, out credentialPtr);

            if (!result)
            {
                throw new Exception("Failed to retrieve credentials.");
            }

            var credential = (CREDENTIAL)Marshal.PtrToStructure(credentialPtr, typeof(CREDENTIAL))!;
            userName = Marshal.PtrToStringUni(credential.UserName)!;

            string password = Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2);
            CredFree(credentialPtr);

            return password;
        }

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredRead(string target, CredentialType type, int reservedFlag, out IntPtr credentialPtr);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CredFree([In] IntPtr cred);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public uint Flags;
            public CredentialType Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        private enum CredentialType : uint
        {
            Generic = 1
        }

        private enum CredentialPersist : uint
        {
            Session = 1,
            LocalMachine = 2,
            Enterprise = 3
        }
    }
}
