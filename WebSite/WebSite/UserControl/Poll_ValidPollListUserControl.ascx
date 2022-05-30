<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Poll_ValidPollListUserControl.ascx.cs"
    Inherits="UserControl_Poll_ValidPollListUserControl" %>


<%--<asp:Image runat="server" ID="ImagePollMoreThanOne" ImageUrl="~/flash/Home/pollMoreThanOne.png"
    Width="100%" Height="33px" Visible="false" />--%>

<%--این یوزر کنترل نیاز به بوتسترپ دارد که در مستر پیج وجود دارد--%>


             <div id="divMultiPoll" runat="server" class="container">
                <div class="mag-title" runat="server" id="div3">
                        <span runat="server" id="Span3">افکارسنجی</span>
                    </div>
                <div class="mag-panel">
               
                         <div class="container">


            <div id="sliderPollUserControl" class="row ">
                <div id="CarouselPollUserControl" class="carousel slide" style="width: 100%" data-ride="carousel">
            
                    <div class="carousel-inner" role="listbox">
                        <asp:Repeater runat="server" ID="RepeaterPoll" >
                            <ItemTemplate>
                                <div class='<%# Container.ItemIndex == 0 ? "item active" : "item" %>'>
                                    <div class="hovereffect2">
                                        <img class="img-responsive" id="ImageNews" runat="server" src='<%# Bind("ImageURL") %>' />
                                        <div class="overlay3">
                                            <h2><%# Eval("Tittle") %>'</h2>
                                            <asp:LinkButton ID="btnTitle" class="info" runat="server" CommandArgument='<%# Bind("PollId") %>'
                                                OnClick="btnTitle_Click" Text='شرکت در نظر سنجی'></asp:LinkButton>
                                            <a   class='<%# Eval("ResultVisibility") %>' href='<%# "/Poll/PollResultMain.aspx?PId="+ Utility.EncryptQS( Eval("PollId").ToString()) %>' target="_blank">نتایج نظرسنجی</a>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#CarouselPollUserControl" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>

                    </a>
                    <a class="right carousel-control" href="#CarouselPollUserControl" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>

                    </a>
                </div>
            </div>

               </div>
                     </div>
             
           </div>


<%--در صفحاتی که از اسلایدر استفاده شده 
    اگر از این یوزرکنترل استفاده می کردیم 
    سبب کانفلیکت می شد بنابراین در هر صفحه که نیاز به حالت اسلایدر داریم باید این اسکریپت به همان صفحه اضافه شود--%>
<%--<script>
    $(document).ready(function () {
        $(".carousel-inner").swiperight(function () {
            $(this).parent().carousel('prev');
        });
        $(".carousel-inner").swipeleft(function () {
            $(this).parent().carousel('next');
        });
    });

    (function () {
        $('.carousel-showmanymoveone .item').each(function () {
            var itemToClone = $(this);

            for (var i = 1; i < 6; i++) {
                itemToClone = itemToClone.next();

                // wrap around if at end of item collection
                if (!itemToClone.length) {
                    itemToClone = $(this).siblings(':first');
                }

                // grab item, clone, add marker class, add to collection
                itemToClone.children(':first-child').clone()
                  .addClass("cloneditem-" + (i))
                  .appendTo($(this));
            }
        });
    }());

    $(window).load(function () {
        // executes when complete page is fully loaded, including all frames, objects and images
        console.log("window is loaded");


        // window load  
    });


</script>--%>
