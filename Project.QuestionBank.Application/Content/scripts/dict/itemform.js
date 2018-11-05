layui.use(['form', 'layer'], function () {
    var form = layui.form;
    layer = parent.layer === undefined ? layui.layer : top.layer;

    //form.on('switch(switchEnabled)', function (data) {
    //    var enableVal = data.elem.checked ? 1 : 0;
    //    $('#EnabledMark').val(enableVal);
    //});

    form.on("submit(btnsave)", function (data) {
        //弹出loading
        var index;
        var url = isNullOrEmpty(data.field.DictItemId) ? '/system/dict/itemform' : '/system/dict/itemform?key=' + data.field.UserId;
        var enableVal = isNullOrEmpty(data.field.EnabledMark) ? 0 : data.field.EnabledMark;
        var isTypeId = isNullOrEmpty(parent.$("#dtypeid").val());
        if (!isTypeId) {
            data.field.DictId = parent.$("#dtypeid").val();
        } else {
            layer.msg("请选择字典类型");
        }
        data.field.EnabledMark = enableVal;
        $.ajax({
            type: 'post',
            url: url,
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
                    parent.reloadData(data.field.DictId);
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