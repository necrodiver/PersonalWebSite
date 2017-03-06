/// <reference path="../echarts/echarts-all.js" />
$(document).ready(function () {
    var visitChart = echarts.init(document.getElementById('visitChart'));
    var signupChart = echarts.init(document.getElementById('signupChart'));
    var visitOption = {
        title: {
            text: '网站访问统计',
            subtext: 'Demo'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            show: true,
            data: ['直接访问', '搜索引擎访问']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: false },
                magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: ['1.8','1.9','1.10','1.11','1.12','1.13','1.14','1.15','1.16','1.17','1.18','1.19','1.20','1.21','1.22','1.23','1.24','1.25','1.26','1.27','1.28','1.29', '1.30', '1.31', '2.1', '2.2', '2.3', '2.4', '2.5', '2.6', '2.7']
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                name: '直接访问',
                type: 'line',
                stack: '总量',
                data: [120,2000,9000,12000, 13002, 10001, 13004, 9000, 23000, 21000, 25300, 28000, 30100,35000,40000,46000,60000,65000,74000,100000,120000,130000,135000,140020,139000,140000,141000,145000,146000,139000,150000]
            },
            {
                name: '搜索引擎访问',
                type: 'line',
                stack: '总量',
                data: [560,1000,15000,22000, 18002, 19001, 23004, 29000, 33000, 31000, 40000, 42500, 45000,47500,50000,67000,75000,85000,90000,96300,100500,110000,113900,124000,130000,130600,140000,149000,153000,160000,168500]
            }
        ]
    };


    signupOption = {
        title: {
            text: '用户登录/注册统计',
            subtext: 'Demo'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            show: true,
            data: ['注册数量', '直接访问数量', '登录访问数量']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                name: '注册数量',
                type: 'line',
                stack: '总量',
                itemStyle: { normal: { areaStyle: { type: 'default' } } },
                data: [1920, 2500, 1020, 1320, 2500, 3400, 6000]
            },
            {
                name: '直接访问数量',
                type: 'line',
                stack: '总量',
                itemStyle: { normal: { areaStyle: { type: 'default' } } },
                data: [3500, 4600, 5100, 7010, 8012, 10001, 10000]
            },
            {
                name: '登录访问数量',
                type: 'line',
                stack: '总量',
                itemStyle: { normal: { areaStyle: { type: 'default' } } },
                data: [350, 400, 510, 710, 802, 1001, 1040]
            }
        ]
    };
    visitChart.setOption(visitOption);
    signupChart.setOption(signupOption);
    $(".form_datetime").datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        language: 'zh-CN'
    });
});