using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IctuTaekwondo.Shared.Schemas.Achievement
{
    public class AchievementUpdateSchema : AchievementCreateSchema
    {
        public int Id { get; set; }
    }
}
