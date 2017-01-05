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
                name: '直接访问',
                type: 'line',
                stack: '总量',
                data: [12000, 13002, 10001, 13004, 9000, 23000, 21000]
            },
            {
                name: '搜索引擎访问',
                type: 'line',
                stack: '总量',
                data: [22000, 18002, 19001, 23004, 29000, 33000, 31000]
            }
        ]
    };


    signupOption = {
        title: {
            text: '用户注册统计',
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
});