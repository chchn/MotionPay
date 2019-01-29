<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MotionPayApi.Pay.Index" %>

<!DOCTYPE html>
<html data-dpr="1" style="font-size: 54px;">
<head lang="en">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0,user-scalable=no" />
    <title>Motion Pay</title>
    <meta name="format-detection" content="telephone=no">
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent">
    <meta name="apple-touch-fullscreen" content="yes">
    <link rel="stylesheet" href="/static/css/wechatpay.css?v=1.04">
    <link rel="stylesheet" href="/static/css/weui.min.css">
    <script src="/static/js/jweixin-1.1.0.js"></script>
    <script src="/static/js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/static/js/layer_mobile/layer.js?v=1.1.1"></script>
    <script type="text/javascript">
        var maxTime = 5000; // 最大查询订单时间5秒钟
        var curTime = 0;
        var aSelIdOld = "";

        $(function () {
            wx.config({
                debug: false,
                appId: "<%=WECHAT_APPID%>",
                jsApiList: ['checkJsApi', 'chooseWXPay']
            });
            wx.ready(function (res) {
                wx.checkJsApi({
                    jsApiList: ['chooseWXPay'],
                    success: function (res) {
                        if (!res.checkResult.chooseWXPay) {
                            alert('当前微信版本不支持所需操作，请更新版本。');
                            return false;
                        }
                    }
                });
            });
        })

        // 支付提交
        function submit() {
            var feeAmount = $("#feeAmount").val();

            //loading带文字
            layer.open({
                type: 2
                , content: 'Loading...'
            });

            $.ajax({
                type: "GET",
                url: "/Pay/Api/CallWxPay.ashx?amount=" + feeAmount,
                data: $("#nofrm").serialize(),
                error: function (request) {
                    alert(request);
                },
                success: function (dataStr) {
                    layer.closeAll();
                    alert("dataStr is:"+dataStr);
                    var data = JSON.parse(dataStr);
                    // var data = dataStr; we have to convert it.
                    alert("code is:" + data.code);
                    if (data.code == 0) {
                        var content = data.content;
                        //alert(content);
                        var paymentPackage = "prepay_id=" + content.prepay_id;
                        var timeStamp = content.timeStamp;
                        var signType = content.signType;
                        var nonceStr = content.nonce_str;
                        var paySign = content.paySign;
                        alert("paymentPackage is:" + paymentPackage);
                        wx.chooseWXPay({
                            "timestamp": timeStamp,
                            "nonceStr": nonceStr,
                            "package": paymentPackage,
                            "signType": signType,
                            "paySign": paySign,
                            success: function (res) {
							alert("SUccess");
							//alert("Scc"+JSON.stringify(res));
                                 alert("success!"+res.stringify());
                                // 支付成功后的回调函数
                                if (res.errMsg == "chooseWXPay:ok") {
                                    alert("支付成功");
                                    setTimeout(function () {
                                        window.location.reload();
                                    }, 2000);
                                }
                                else if (res.errMsg == "chooseWXPay:fail") {
                                    alert("支付失败");
                                }
                                else if (res.errMsg == "chooseWXPay:cancel") {
                                    alert("已取消支付");
                                }
                                else {
                                    alert("支付访问被拒绝:商户设置错误");
                                }
                            },
                            fail: function (res) {
                                alert("fail!" + res.errMsg);
                            }
                        });
                    } else if (data.code == -1) {
                        // alert("code is: -1");
                        alert(data.message);
                    } else if (data.code == -2) {
                        // alert("code is: -2");
                        alert(data.message);
                        wx.closeWindow();
                    } else {
                        // alert("code is something else.");
                        alert(data.message);
                    }
                }
            });
        }


        function ChangeAmount() {
            var rate = $("#Rate").html();
            var amount = $("#feeAmount").val();
            var rmbAmount = parseFloat(rate) * parseFloat(amount);
            $("#rmbAmount").html(rmbAmount.toFixed(2));
        }
    </script>
</head>
<body style="font-size: 12px;">
    <div id="wrap">
        <div id="front">
            <header>
                <div class="logo">
                    <img src="/static/images/Motionpay-Logo.gif"><h1>MotionPay Demo</h1>
                </div>
            </header>
            <section>
                <div class="amountArea">
                    <span style="font-size: 15px;">金额</span>
                    $<input type="text" class="amount" id="feeAmount" value="0.01" oninput="ChangeAmount()" />
                </div>
                <div class="ratebox">
                    <div>汇率:$1≈￥<span id="Rate">5.6</span></div>
                    <p style="padding-right: 0.3rem;">≈￥<span id="rmbAmount">0.05</span></p>
                </div>
                <h3>
                    <span>Powered by Motion Pay</span>
                </h3>
            </section>
            <footer>
                <div id="" class="keyBoard show-trans show " onclick="submit()">
                    <i class="payactive">微信支付</i>
                </div>
            </footer>
        </div>
    </div>
</body>
</html>
