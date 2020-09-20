<%@ Page Title="Cписок юр.лиц" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JuridicalPersonList.aspx.cs" Inherits="LegalPersonsProject.JuridicalPersonList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                    <h2><%: Page.Title %></h2>
                </hgroup>

                <asp:ObjectDataSource
                    ID="JuridicalPersonObjectDataSource"
                    runat="server"
                    TypeName="LegalPersonsProject.Models.JuridicalPersonRepository"
                    SortParameterName="SortColumns"
                    EnablePaging="true"
                    SelectMethod="GetAll"
                    DeleteMethod="DeleteJuridicalPerson">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" DbType="Guid" />
                    </DeleteParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource
                    ID="JuridicalPersonContactsObjectDataSource"
                    runat="server"
                    TypeName="LegalPersonsProject.Models.JuridicalPersonRepository"
                    OldValuesParameterFormatString="{0}"
                    SelectMethod="GetContactsById" InsertMethod="">
                    <SelectParameters>
                        <asp:Parameter Name="id" DBType="Guid" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                

                <asp:ObjectDataSource
                    ID="SearchJuridicalPersonObjectDataSource"
                    runat="server"
                    TypeName="LegalPersonsProject.Models.JuridicalPersonRepository"
                    SelectMethod="FindByBinOrIin">
                    <SelectParameters>
                        <asp:Parameter Name="binOrIin" Type="Int64" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <div>
                    <asp:Label runat="server" Text="Поиск по БИН: " style=""></asp:Label>
                    <asp:TextBox ID="SearchTextBox" runat="server" OnTextChanged="SearchTextBox_TextChanged" />
                    <asp:Button ID="SearchButton" runat="server" CommandName="FIND" OnClick="SearchTextBox_TextChanged" Text="Искать" />
                </div>
                
                <asp:GridView runat="server" ID="JuridicalPersonsGridView"
                    DataSourceID="JuridicalPersonObjectDataSource"
                    DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    AllowSorting="true"
                    AllowPaging="true"
                    PageSize="20"
                    OnSelectedIndexChanged="JuridicalPersonsGridView_SelectedIndexChanged"
                    >
                    <HeaderStyle backcolor="lightblue" ForeColor="black"/>
                    <Columns>
                        <asp:ButtonField Text="Контакты" HeaderText="Contacts" CommandName="SELECT"/>
                        <asp:HyperLinkField Text="Детали" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~\JuridicalPersonDetails.aspx?id={0}" HeaderText="Детали" Target="_blank"/>
                        <asp:BoundField DataField="Id" InsertVisible="False" ReadOnly="true" Visible="false"/>
                        <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name, CreatedAt"/>
                        <asp:BoundField DataField="BINorIIN" HeaderText="БИН" SortExpression="BINorIIN, Name"/>
                        <asp:BoundField DataField="CreatedAt" HeaderText="Дата создания" InsertVisible="false" SortExpression="CreatedAt, UpdatedAt"/>          
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Дата изменения" InsertVisible="false" SortExpression="UpdatedAt, CreatedAt"/>                 
                        <asp:BoundField DataField="CreatedBy" HeaderText="Автор" SortExpression="CreatedBy, UpdatedBy"/>          
                        <asp:BoundField DataField="UpdatedBy" HeaderText="Автор изменения" SortExpression="UpdatedBy, CreatedBy"/>        
                        <asp:ButtonField Text="Удалить" CommandName="DELETE" />
                    </Columns>
                </asp:GridView>

                <asp:GridView
                    ID="JuridicalPersonContactsGridView"
                    runat="server"
                    DataSourceID="JuridicalPersonContactsObjectDataSource"
                    AutoGenerateColumns="false" 
                    EmptyDataText="Нет контактов.">
                    <HeaderStyle backcolor="lightblue" ForeColor="black"/>
                    <Columns>
                        <asp:BoundField DataField="PhysicalPersonId" Visible="false"  />
                        <asp:BoundField DataField="Name" HeaderText="Имя" ReadOnly="true" />
                        <asp:BoundField DataField="Secondname" HeaderText="Отчество" ReadOnly="true" />
                        <asp:BoundField DataField="Lastname" HeaderText="Фамилия" ReadOnly="true" />
                        <asp:BoundField DataField="BINorIIN" HeaderText="ИИН" ReadOnly="true" />
                        <asp:BoundField DataField="CreatedAt" HeaderText="Дата создания" ReadOnly="true" />
                        <asp:BoundField DataField="UpdatedAt" HeaderText="Дата изменения" ReadOnly="true" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Автор" ReadOnly="true" />
                        <asp:BoundField DataField="UpdatedBy" HeaderText="Автор изменения" ReadOnly="true" />
                        <asp:BoundField DataField="Position" />
                    </Columns>
                </asp:GridView>
                <asp:HyperLink Text="Создать юр. лицо" runat="server" NavigateUrl="~/JuridicalPersonDetails.aspx?create=true">
                </asp:HyperLink>
                
        </div>
    </section>
</asp:Content>
