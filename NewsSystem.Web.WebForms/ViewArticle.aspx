<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewArticle.aspx.cs" Inherits="NewsSystem.Web.WebForms.ViewArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView runat="server" ID="fvDetails" ItemType="NewsSystem.Data.Models.Article" SelectMethod="fvDetails_GetItem">
        <HeaderTemplate>
            <newsSystem:LikeHateCtrl runat="server" ID="likeHate" Visible="<%#System.Web.HttpContext.Current.User.Identity.IsAuthenticated %>" />
        </HeaderTemplate>
        <ItemTemplate>
            <h1><%#:Item.Title %> <small>Category: <%#:Item.Category.Name %></small></h1>
            <p><%#:Item.Content %></p>
            <p>Author: <%#:Item.Author.UserName %></p>
            <p class="pull-right"><%#Item.DateCreated %></p>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
