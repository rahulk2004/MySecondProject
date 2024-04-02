<%@ Page Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UserAddService.aspx.cs" Inherits="User_UserAddService" Title="Add Service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
           <style>
    .form-container {
        background-color: #f8f9fa;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        width: 60%;
        box-sizing: border-box;
        text-align: left;
        margin: auto;
    }

    h2 {
        color: #333;
    }

    label {
        display: block;
        margin-bottom: 5px;
        color: #555;
    }

    
    .form-container input[type="text"],
    .form-container textarea,
    .form-container select {
        width: calc(100% - 16px);
        padding: 8px;
        margin-bottom: 10px;
        box-sizing: border-box;
        border: 1px solid #ced4da;
        border-radius: 4px;
        display: inline-block;
    }

    
    .form-container input[type="text"],
    .form-container textarea {
        display: block;
    }

    

    .validation-summary-errors {
        color: #ff0000;
        margin-bottom: 10px;
    }

    .button-container {
        text-align: center;
        margin-top: 15px;
    }

    .btnAddService {
        background-color: #007bff;
        color: white;
        padding: 10px 15px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    .btnAddService:hover {
        background-color: #0056b3;
    }

    .file-upload-container {
        margin-top: 15px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-container">
        <h2>Add New Service</h2>
        <asp:Label ID="lblServiceName" runat="server" Text="Service Name:"></asp:Label>
        <asp:TextBox ID="txtServiceName" runat="server" MaxLength="20"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvServiceName" runat="server"
            ControlToValidate="txtServiceName"
            ErrorMessage="Please enter a service name."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" 
            MaxLength="50"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
            ControlToValidate="txtDescription"
            ErrorMessage="Please enter a description."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <br />
        
        <asp:Label ID="Label1" runat="server" Text="Category:"></asp:Label>
        <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Select Existing Category" Value="" />
            <asp:ListItem Text="Add New Category" Value="AddNew" />
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server" placeholder="New Category" 
            Enabled="false" MaxLength="15"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
            ControlToValidate="ddlCategory"
            ErrorMessage="Please select an existing category or enter a new one."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvNewCategory" runat="server"
            ControlToValidate="TextBox1"
            InitialValue=""
            ErrorMessage="Please enter a new category."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="Label2" runat="server" Text="Subcategory:"></asp:Label>
        <asp:DropDownList ID="ddlSubcategory" runat="server" OnSelectedIndexChanged="ddlSubcategory_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Select Existing Subcategory" Value="" />
            <asp:ListItem Text="Add New Subcategory" Value="AddNew" />
        </asp:DropDownList>
        <asp:TextBox ID="txtSubcategory" runat="server" placeholder="New Subcategory" MaxLength="15" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvSubcategory" runat="server"
            ControlToValidate="ddlSubcategory"
            ErrorMessage="Please select a subcategory or enter a new one."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvTxtSubcategory" runat="server"
            ControlToValidate="txtSubcategory"
            ErrorMessage="Please enter a new subcategory."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revTxtSubcategory" runat="server"
            ControlToValidate="txtSubcategory"
            ErrorMessage="Invalid subcategory format."
            ValidationExpression="^[a-zA-Z0-9\s]+$"
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RegularExpressionValidator>
        <br />

        <asp:Label ID="Label3" runat="server" Text="Location:"></asp:Label>
        <asp:DropDownList ID="ddlLocation" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Select Existing Location" Value="" />
            <asp:ListItem Text="Add New Location" Value="AddNew" />
        </asp:DropDownList>
        <asp:TextBox ID="TextBox3" runat="server" placeholder="New Location" MaxLength="10" Enabled="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLocation" runat="server"
            ControlToValidate="ddlLocation"
            ErrorMessage="Please select an existing location or enter a new one."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvNewLocation" runat="server"
            ControlToValidate="TextBox3"
            ErrorMessage="Please enter a new location."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <br />


        <asp:Label ID="lblPrice" runat="server" Text="Price:"></asp:Label>
        <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regexPrice" runat="server"
    ControlToValidate="txtPrice"
    ErrorMessage="Please enter a valid numeric value."
    ValidationExpression="^\d+(\.\d{1,2})?$"
    Display="Dynamic" ForeColor="Red">
</asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
            ControlToValidate="txtPrice"
            ErrorMessage="Please enter a price."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        
        <br />

        <asp:Label ID="lblImage" runat="server" Text="Service Image:"></asp:Label>
        <asp:FileUpload ID="FileUploadService" runat="server" />
        <asp:RequiredFieldValidator ID="rfvFileUploadService" runat="server"
            ControlToValidate="FileUploadService"
            ErrorMessage="Please select a service image."
            Display="Dynamic"
            SetFocusOnError="true"
            EnableClientScript="true"
            ValidationGroup="AddServiceValidation">
        </asp:RequiredFieldValidator>
        <br />

        <asp:Button ID="btnAddService" runat="server" Text="Add Service" OnClick="btnAddService_Click" ValidationGroup="AddServiceValidation" />
    </div>
</asp:Content>
