﻿layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        laytpl = layui.laytpl,
        table = layui.table;

    table.render({
        elem: '#userlist',
        id: "userlist",
        url: '/user/users',
        height: 'full-105',
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
        cols: [[
            { type: 'checkbox' },
            { field: 'UserId', width: 80, title: 'ID', sort: false },
            { field: 'UserName', width: 120, title: '用户名' },
            { field: 'RealName', width: 120, title: '姓名', sort: false },
            { field: 'RoleName', width: 100, title: '当前角色', templet: '#role' },
            { field: 'Age', width: 80, title: '年龄' },
            { field: 'Email', title: '电子邮箱', minWidth: 150 },
            //{ field: 'CreateTime', title: '添加时间', minWidth: 150, templet: '#createTime' },
            { field: 'Remark', width: 150, title: '备注信息', sort: false },
            { fixed: "right", title: '操作', minWidth: 150, toolbar: '#userops', align: "left" }
        ]]
    });

    function addUser(key) {
        var title = isNullOrEmpty(key) ? '新增用户' : '编辑用户';
        var index = layui.layer.open({
            title: title,
            type: 2,
            content: '/user/form',
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

    //新增
    $(".addNews_btn").click(function () {
        addUser();
    });

    //查询
    $(".search_btn").on("click", function () {
        table.reload("userlist", {
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: {
                keyword: $(".searchVal").val()  //搜索的关键字
            }
        });
    });

    //批量删除
    $(".btn_delAll").click(function () {
        var checkStatus = table.checkStatus('userlist'),
            data = checkStatus.data,
            ids = [];
        if (data.length > 0) {
            for (var i in data) {
                ids.push(data[i].UserId);
            }
            layer.confirm('确定删除选中的用户？', { icon: 3, title: '提示信息' }, function (index) {
                // $.get("删除文章接口",{
                //     newsId : newsId  //将需要删除的newsId作为参数传入
                // },function(data){
                layer.msg(ids.length);
             
                table.reload();
                layer.close(index);
                // })
            });
        } else {
            layer.msg("请选择需要删除的用户");
        }
    });

    //列表操作
    table.on('tool(userlist)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { //编辑
            addUser(data.UserId);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此用户？', { icon: 3, title: '提示信息' }, function (index) {
                $.post("/user/delete", { key: data.UserId }, function (res) {
                    if (res.status === '1') {
                        top.layer.msg(res.msg);
                        layer.close(index);
                        location.reload();
                    } else {
                        top.layer.msg(res.msg);
                        layer.close(index);
                        return false;
                    }
                });
            });
        }
    });
});

//对Date的扩展，将 Date 转化为指定格式的String
//月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，
//年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
//例子：
//(new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423
//(new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1,
                (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};