layui.use(['form', 'layer', 'table', 'laytpl'],
    function () {
        var form = layui.form,
            layer = parent.layer === undefined ? layui.layer : top.layer,
            laytpl = layui.laytpl,
            table = layui.table;

        table.render({
            elem: '#dictlist',
            id: "dictlist",
            url: '/user/users',
            height: 'full-185',
            cellMinWidth: 80,
            limit: 15,
            limits: [10, 15, 20, 30, 40, 50, 100, 200, 500],
            page: { //支持传入 laypage 组件的所有参数（某些参数除外，如：jump/elem） - 详见文档
                layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'], //自定义分页布局
                //,curr: 5 //设定初始在第 5 页
                groups: 6, //只显示 1 个连续页码
                first: false, //不显示首页
                last: false //不显示尾页
            },
            request: {
                pageName: 'page', //页码的参数名称，默认：page
                limitName: 'size' //每页数据量的参数名，默认：limit
            },
            cols: [
                [
                    { type: 'checkbox' },
                    { field: 'DictItemId', width: 80, title: '主键', hide: true },
                    { field: 'ItemName', width: 100, title: '项目名称' },
                    { field: 'ItemVal', width: 80, title: '项目值', sort: false },
                    { field: 'ItemPy', width: 80, title: '简拼' },
                    { field: 'SortCode', width: 70, title: '排序' },
                    { field: 'Enable', title: '有效', width: 80 },
                    //{ field: 'CreateTime', title: '添加时间', minWidth: 150, templet: '#createTime' },
                    { field: 'Remark', width: 120, title: '备注', sort: false },
                    { fixed: "right", title: '操作', minWidth: 120, toolbar: '#userops', align: "left" }
                ]
            ]
        });
    });

$(document).ready(function () {
    var bodyHeight = $(document).height();
    $('.layui-card').css('height', bodyHeight - 50 + 'px');
    $.fn.zTree.init($("#dictTypeTree"), setting);
    $(window).load(function () {
        $(".cardcontent").mCustomScrollbar({
            axis: "yx",
            theme: "dark"
        });
    });
});

var setting = {
    data: {
        simpleData: {
            enable: true,
            idKey: 'DictId',
            pIdKey: 'ParentId',
            rootPId: '0'
        },
        key: {
            name: 'DictName'
        }
    },
    async: {
        enable: true,
        type: "get",
        dataType: "json",
        url: "/dict/treelist",
        dataFilter: filter
    },
    callback: {
        beforeClick: zTreeBeforeClick,
        onClick: zTreeOnClick
    }
};

function filter(treeId, parentNode, responseData) {
    if (responseData) {
        for (var i = 0; i < responseData.length; i++) {
            responseData[i].DictName += '【' + responseData[i].DictCode + '】';
            if (responseData[i].IsNav === 1) {
                responseData[i].open = true;
            }
        }
    }
    return responseData;
}

function zTreeOnAsyncSuccess(event, treeId, treeNode, msg) {
    if (treeNode.IsNav === 1) {
        treeNode.open = true;
    }
}

function zTreeBeforeClick(treeId, treeNode, clickFlag) {
    return (treeNode.IsNav !== 1);
}

function zTreeOnClick(event, treeId, treeNode) {
    $('#dtypeid').val(treeNode.DictId);
    $('#dtypename').text(treeNode.DictName);
    //设置表格显示及标题提示 加载表格数据
}