var form,layer,laytpl,table;
var typeId = $('#dtypeid').val();
layui.use(['form', 'layer', 'table', 'laytpl'],
    function () {
        form = layui.form,
            layer = parent.layer === undefined ? layui.layer : top.layer,
            laytpl = layui.laytpl,
            table = layui.table;

        table.render({
            elem: '#dictlist',
            id: "dictlist",
            url: '/system/dict/itembytype',
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
            where: { typeId: typeId },
            request: {
                pageName: 'page', //页码的参数名称，默认：page
                limitName: 'size' //每页数据量的参数名，默认：limit
            },
            cols: [
                [
                    { type: 'checkbox' },
                    { field: 'DictItemId', width: 80, title: '主键', hide: true },
                    { field: 'ItemName', width: 100, title: '项目名称' },
                    { field: 'ItemCode', width: 100, title: '项目代码' },
                    { field: 'ItemValue', width: 80, title: '项目值', sort: false },
                    { field: 'SimpleSpelling', width: 80, title: '简拼' },
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

    //新增项目
    $(".addItem_btn").click(function () {
        typeId = $('#dtypeid').val();
        if (!typeId) {
            layer.msg("请选择字典类型");
            return;
        }
        addItem();
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
    table.reload("dictlist", {
        page: {
            curr: 1 //重新从第 1 页开始
        },
        where: {
            typeId: treeNode.DictId //字典类型
        }
    });
}

function addItem(key) {
    var title = isNullOrEmpty(key) ? '新增项目' : '编辑项目';
    var index = layui.layer.open({
        id:'dicttype',
        title: title,
        type: 2,
        content: '/system/dict/itemform',
        success: function (layero, index) {
            var body = layui.layer.getChildFrame('body', index);
            if (key) {
                $.get('/user/getusermodel?key=' + key, function (res) {
                    //body.find(".UserName").val(res.UserName);
                    body.find('.layui-form').formSerialize(res);
                    form.render();
                });
                //    body.find(".userName").val(edit.userName);  //登录名
                //    body.find(".userEmail").val(edit.userEmail);  //邮箱
                //    body.find(".userSex input[value=" + edit.userSex + "]").prop("checked", "checked");  //性别
                //    body.find(".userGrade").val(edit.userGrade);  //会员等级
                //    body.find(".userStatus").val(edit.userStatus);    //用户状态
                //    body.find(".userDesc").text(edit.userDesc);    //用户简介
                //    form.render();
            }
            setTimeout(function () {
                layui.layer.tips('点击此处返回数据列表', '.layui-layer-setwin .layui-layer-close', {
                    tips: 3
                });
            }, 500);
        }
    });
    layui.layer.full(index);
    window.sessionStorage.setItem("index", index);
    //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
    $(window).on("resize", function () {
        layui.layer.full(window.sessionStorage.getItem("index"));
    });
}