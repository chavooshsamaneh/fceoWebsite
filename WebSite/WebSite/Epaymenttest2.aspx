<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Epaymenttest2.aspx.cs" Inherits="Epaymenttest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <div class="col-sm-6 col-sm-offset-3 m-t-xs-40">
                    <form method="post" action="https://ikc.shaparak.ir/TPayment/Payment/Index">
                 
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>مقدار-ریال</label>
                                    1000

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>توکن مشتری</label>
                                    <input id="token" name="token"  class="form-control" runat="server" />
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>شناسه پذیرنده</label>
                                    <input id="merchantId" name="merchantId" class="form-control" runat="server"/>
                                </div>
                            </div>
                        </div>

                       


                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <button type="submit" class="btn btn-success ben-block form-control">پرداخت</button>
                                </div>
                            </div>

                        </div>

                    </form>
                </div>
</body>
</html>
