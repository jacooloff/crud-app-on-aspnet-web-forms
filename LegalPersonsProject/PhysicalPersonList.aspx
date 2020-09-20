<%@ Page Title="Список физ.лиц" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhysicalPersonList.aspx.cs" Inherits="LegalPersonsProject.PhysicalPersonList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script runat="server">
        

    </script>
    <section>
        <div>
            
                <hgroup>
                    <h2><%: Page.Title %></h2>
                </hgroup>
                
                <asp:ObjectDataSource
                    ID="PhysicalPersonObjectDataSource"
                    runat="server"
                    TypeName="LegalPersonsProject.Models.PhysicalPersonRepository"
                    SortParameterName="SortColumns"
                    EnablePaging="true"
                    SelectMethod="GetAll"
                    DeleteMethod="DeletePhysicalPerson">
                </asp:ObjectDataSource>
                
                <asp:ObjectDataSource
                    ID="SearchPhysicalPersonObjectDataSource"
                    runat="server"
                    TypeName="LegalPersonsProject.Models.PhysicalPersonRepository"
                    SelectMethod="FindByBinOrIin">
                    <SelectParameters>
                        <asp:Parameter Name="binOrIin" Type="Int64" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <div>
                    <asp:Label runat="server" Text="Поиск по ИИН: " style=""></asp:Label>
                    <asp:TextBox ID="SearchTextBox" runat="server" OnTextChanged="SearchTextBox_TextChanged" />
                    <asp:Button ID="SearchButton" runat="server" CommandName="FIND" OnClick="SearchTextBox_TextChanged" Text="Искать" />
                </div>
                
                
                <asp:GridView runat="server" ID="PhysicalPersonsGridView"
                    DataSourceID="PhysicalPersonObjectDataSource"
                    DataKeyNames="Id"
                    AutoGenerateColumns="false" 
                    AllowSorting="true"
                    AllowPaging="true"
                    PageSize="20">
                    <HeaderStyle backcolor="lightblue" ForeColor="black"/>
                    <Columns>
                        <asp:HyperLinkField Text="Детали" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/PhysicalPersonDetails.aspx?id={0}" />
                        <asp:BoundField DataField="Id" InsertVisible="False" ReadOnly="true" visible="false"/>
                        <asp:BoundField DataField="Lastname" HeaderText="Фамилия" SortExpression="Lastname, Name"/>
                        <asp:BoundField DataField="Name" HeaderText="Имя" SortExpression="Name, Lastname" /> 
                        <asp:BoundField DataField="Secondname" HeaderText="Отчество" SortExpression="Secondname, Name"/> 
                        <asp:BoundField DataField="BINorIIN" HeaderText="ИИН" SortExpression="BINorIIN, Name"/> 
                        <asp:BoundField DataField="CreatedAt" HeaderText="Дата создания" SortExpression="CreatedAt, UpdatedAt"/>          
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Дата изменения" SortExpression="UpdatedAt, CreatedAt"/>                 
                        <asp:BoundField DataField="CreatedBy" HeaderText="Автор" SortExpression="CreatedBy, UpdatedBy"/>          
                        <asp:BoundField DataField="UpdatedBy" HeaderText="Автор изменения" SortExpression="UpdatedBy, CreatedBy"/>   
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                 
                </asp:GridView>
                <asp:HyperLink Text="Создать физ. лицо" runat="server" NavigateUrl="~/PhysicalPersonDetails.aspx?create=true">
                </asp:HyperLink>
                
            
        </div>
    </section>

</asp:Content>
