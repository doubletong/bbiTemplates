<%@ Page Language="C#" MasterPageFile="~/TBHMain.master"  Theme="DarkBeer" AutoEventWireup="true" 
CodeFile="ShowProduct.aspx.cs" Inherits="ShowProduct" %>

<%@ Register Assembly="BBICMSBLL" Namespace="BBICMS" TagPrefix="asp" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <link href="/Content/Public/Plugins/jquery.bxslider/jquery.bxslider.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
   
        <div class="sitepath"><i class="icon-home"></i> <a href="/">首页</a> / 模板</div>
    
   <div class="pure-g">
        <div class="pure-u-17-24">
            <section class="mr20">           
     

            <div id="propics">
                    <ul class="bxslider">
                        <asp:Repeater ID="rpProPhotos" runat="server">
                            <ItemTemplate>
                                <li>
                                    <img src="<%# BBICMS.StoreHelper.GetPhotosUrl(Eval("OriginalPic").ToString()) %>" 
                                        alt="" /></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    
                </div>
            

 
           
            <div id="productinfo" class="mb50">
             <%--   <p>Rating: 
                <asp:Literal runat="server" ID="lblRating" Text="{0} user(s) have rated this product " />
                 </p>--%>
                <asp:Literal runat="server" ID="lblDescription" />
              
                <%--b>Availability: </b>
                <asp:AvailabilityImage runat="server" ID="availDisplay" />
                <br />
                
                <br />--%>
                
               <%-- <p  class="entry-content"><asp:HyperLink runat="server" ID="lnkFullImage" Target="_blank"></asp:HyperLink>
                </p>--%>
               <%-- <br />
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Shopping Cart" />
                <br />--%>
               
              <%--  <div class="sectiontitle">
                    How would you rate this product?
                   
                            Rate This Product:<br />
                  
                            <asp:Rating ID="ProductRating" runat="server" BehaviorID="ratDisplay" CssClass="ArticleRating"
                                StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                EmptyStarCssClass="emptyRatingStar">
                            </asp:Rating>
                      
                </div>
                <asp:Literal runat="server" ID="ltlAvgRating" Text="The average rating for {1} is {0} beer(s)." />
                <asp:Literal runat="server" ID="lblUserRating" Visible="False" Text="Your rated this product {0} beer(s). Thank you for your feedback." />--%>
            
        </div>
                </section>
    </div>
       <div class="pure-u-7-24">
           <section class="ml20">
            <h3 class="title"><asp:Label runat="server" ID="lblTitle" /></h3>
           <div class="price">
          
               <p>
                <asp:Literal runat="server" ID="lblDiscountedPrice"><s>{0}</s> {1}% Off = </asp:Literal>
               <i class="icon-yen"></i> <asp:Literal runat="server" ID="lblPrice" />
                   </p>
          </div>

           <div id="bx-pager" class="clearfix">
                        <asp:Repeater ID="rpThumbs" runat="server">
                            <ItemTemplate>
                                <a href="#" data-slide-index='<%#DataBinder.Eval(Container, "ItemIndex", "")%>' >
                                    <img src='<%#  BBICMS.StoreHelper.GetPhotosThumbUrl(Eval("Thumbnail").ToString()) %>'
                                        /></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

         <%--  <uc1:ShoppingCartBox ID="ShoppingCartBox1" runat="server" />
            <uc1:PollBox ID="PollBox1" runat="server" />--%>
               </section>
       </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHFooter" runat="Server">
    <script src="/Content/Public/Plugins/jquery.bxslider/jquery.bxslider.min.js"></script>
    <script src="/Content/Public/Plugins/jquery.bxslider/plugins/jquery.easing.1.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //当前菜单
            //var $thirdLink = $("#page_header nav");
            //$thirdLink.find('li').eq(3).find('a').addClass("active");


            $('.bxslider').bxSlider({
                pagerCustom: '#bx-pager',
                adaptiveHeight: true,
                mode: 'fade'
            });


        });
        </script>
</asp:Content>