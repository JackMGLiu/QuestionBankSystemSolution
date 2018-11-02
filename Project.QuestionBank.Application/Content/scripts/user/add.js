layui.use(['form', 'layer'], function () {
    var form = layui.form;
    layer = parent.layer === undefined ? layui.layer : top.layer;

    form.on("submit(addUser)", function (data) {
        //弹出loading
        var index;
        $.ajax({
            type: 'post',
            url: '/User/Add',
            dataType: 'json',
            data: data.field,
            beforeSend: function () {
                index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
            },
            complete: function () {
                top.layer.close(index);
            },
            success: function (res) {
                if (res.status === '1') {
                    top.layer.msg(res.msg);
                    layer.closeAll("iframe");
                    //刷新父页面
                    parent.location.reload();
                } else {
                    top.layer.msg(res.msg);
                    return false;
                }
            }
        });
        //console.log('formdata', data.field);
        //setTimeout(function () {
        //    top.layer.close(index);
        //    top.layer.msg("用户添加成功！");
        //    layer.closeAll("iframe");
        //    //刷新父页面
        //    parent.location.reload();
        //}, 2000);
        return false;
    });
});