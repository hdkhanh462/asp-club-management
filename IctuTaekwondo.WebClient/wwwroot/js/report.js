function fetchChartData(year) {
    var apiUrl = $("#api-url").val()

    return $.ajax({
        url: `${apiUrl}/api/finances/report-year?year=${year}`,
        method: 'GET',
        dataType: 'json'
    });
}

function barChart() {
    let options = {
        series: [
            {
                name: "Thu nhập",
                color: "#31C48D",
                data: ["1420", "1620", "1820", "1420", "1650", "2120"],
            },
            {
                name: "Chi tiêu",
                data: ["788", "810", "866", "788", "1100", "1200"],
                color: "#F05252",
            }
        ],
        chart: {
            sparkline: {
                enabled: false,
            },
            type: "bar",
            width: "100%",
            height: 400,
            toolbar: {
                show: false,
            }
        },
        fill: {
            opacity: 1,
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: "100%",
                borderRadiusApplication: "end",
                borderRadius: 6,
                dataLabels: {
                    position: "top",
                },
            },
        },
        legend: {
            show: true,
            position: "bottom",
        },
        dataLabels: {
            enabled: false,
        },
        tooltip: {
            shared: true,
            intersect: false,
            formatter: function (value) {
                return value.toLocaleString() + "đ"
            }
        },
        xaxis: {
            labels: {
                show: true,
                style: {
                    fontFamily: "Inter, sans-serif",
                    cssClass: 'text-xs font-normal fill-gray-500 dark:fill-gray-400'
                },
            },
            categories: ["Thg01", "Thg02", "Thg03", "Thg04", "Thg05", "Thg06"],
            axisTicks: {
                show: false,
            },
            axisBorder: {
                show: false,
            },
        },
        yaxis: {
            labels: {
                show: true,
                style: {
                    fontFamily: "Inter, sans-serif",
                    cssClass: 'text-xs font-normal fill-gray-500 dark:fill-gray-400'
                },
                formatter: function (value) {
                    return value.toLocaleString() + "đ"
                }
            }
        },
        grid: {
            show: true,
            strokeDashArray: 4,
            padding: {
                left: 2,
                right: 2,
                top: -20
            },
        },
        fill: {
            opacity: 1,
        }
    }

    const year = new Date().getFullYear();
    fetchChartData(year)
        .done(function (data) {
            options["series"] = data.series;
            options.xaxis["categories"] = data.categories;

            if (document.getElementById("bar-chart") && typeof ApexCharts !== 'undefined') {
                const chart = new ApexCharts(document.getElementById("bar-chart"), options);
                chart.render();
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error('Error fetching chart data:', textStatus, errorThrown);
        });
}

barChart();