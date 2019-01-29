function LayerAlert(content) {
    layer.open({
        content: content
        , skin: 'msg'
        , time: 2 //2秒后自动关闭
    });
}
function LayerLoading()
{
    //loading带文字
    layer.open({
        type: 2
        , content: '加载中...'
    });
}
function LayerClose()
{
	layer.closeAll();
}