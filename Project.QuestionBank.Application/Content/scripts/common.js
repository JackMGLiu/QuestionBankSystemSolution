//setTimeout(function () {
//    $('.page-loading').remove();
//}, window === top ? 650 : 450);

$(document).ready(function () {
    //监听加载状态改变
    document.onreadystatechange = completeLoading;
});

//加载状态为complete时移除loading效果
function completeLoading() {
    if (document.readyState === "complete") {
        $('.page-loading').remove();
    }
}

//form表单自动赋值
$.fn.formSerialize = function (formdata) {
    var element = $(this);
    if (!!formdata) {
        for (var key in formdata) {
            //var $id = element.find('#' + key);
            var $id = element.find('.' + key);
            var value = $.trim(formdata[key]).replace(/ /g, '');
            var type = $id.attr('type');
            if ($id.hasClass("select2-hidden-accessible")) {
                type = "select";
            }
            switch (type) {
                case "checkbox":
                    if (value === "true") {
                        $id.attr("checked", 'checked');
                    } else {
                        $id.removeAttr("checked");
                    }
                    break;
                case "select":
                    $id.val(value).trigger("change");
                    break;
                default:
                    $id.val(value);
                    break;
            }
        };
        return false;
    }
    var postdata = {};
    element.find('input,select,textarea').each(function (r) {
        var $this = $(this);
        var id = $this.attr('id');
        var type = $this.attr('type');
        switch (type) {
            case "checkbox":
                postdata[id] = $this.is(":checked");
                break;
            default:
                var value = $this.val() === "" ? " " : $this.val();
                if (!$.request("keyValue")) {
                    value = value.replace(/ /g, '');
                }
                postdata[id] = value;
                break;
        }
    });
    return postdata;
};