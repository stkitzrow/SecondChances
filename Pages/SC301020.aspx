<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="SC301020.aspx.cs" Inherits="Page_SC301020" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource EnableAttributes="true" ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.SecondChances.SecondChancesMaint" PrimaryView="Document">
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Document"
        CaptionVisible="False" TabIndex="100" ActivityIndicator="True" NoteIndicator="True" FilesIndicator="True" BPEventsIndicator="True" >
        <Template>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule55" StartRow="True" />
	<px:PXTextEdit runat="server" DataField="ObjectCD" ID="CstPXMaskEdit29" />
            <px:PXLayoutRule runat="server" ID="CstPXLayoutRule8" StartRow="True" ></px:PXLayoutRule>
            <px:PXTextEdit Width="" runat="server" ID="CstPXTextEdit5" DataField="Descr" ></px:PXTextEdit>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule11" StartRow="True" ></px:PXLayoutRule>
	<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown12" DataField="CurrentDocument.Status" ></px:PXDropDown>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule24" StartRow="True" ></px:PXLayoutRule>
	<px:PXSelector runat="server" ID="CstPXSelector25" DataField="WorkgroupID" ></px:PXSelector>
	<px:PXSelector runat="server" ID="CstPXSelector33" DataField="OwnerID" ></px:PXSelector>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule27" StartColumn="True" ></px:PXLayoutRule>
	<px:PXTextEdit runat="server" ID="CstPXTextEdit26" DataField="WorkgroupID_description" ></px:PXTextEdit>
	<px:PXTextEdit runat="server" ID="CstPXTextEdit34" DataField="OwnerID_description" ></px:PXTextEdit></Template>
    </px:PXFormView>
   <px:PXTab DataMember="CurrentDocument" runat="server" ID="MainTab">
		<Items>
			<px:PXTabItem Text="Pictures" >
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" ></px:PXLayoutRule>
                    <px:PXImageUploader Height="320px" Width="430px" ID="imgUploader" runat="server" DataField="ImageUrl" AllowUpload="true" AllowNoImage="true" ShowComment="true" DataMember="CurrentDocument"
						 ></px:PXImageUploader>
            </Template>
            </px:PXTabItem>
			<px:PXTabItem Text="Detail Description" >
                <Template>
                    <px:PXRichTextEdit ID="edBody" runat="server" DataField="Body" Style="border-width: 0px; border-top-width: 1px; width: 100%;"
                        AllowAttached="true" AllowSearch="true" AllowLoadTemplate="false" AllowSourceMode="true">
                        <AutoSize Enabled="True" MinHeight="216" ></AutoSize>
                    </px:PXRichTextEdit>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="General" >
				<Template>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule13" StartColumn="True" ></px:PXLayoutRule>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule22" StartGroup="True" GroupCaption="From Location" ></px:PXLayoutRule>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask4" DataField="CustomerID" ></px:PXSegmentMask>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask6" DataField="CustomerLocationID" ></px:PXSegmentMask>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule23" StartGroup="True" GroupCaption="Item Information" ></px:PXLayoutRule>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask8" DataField="ItemClassID" ></px:PXSegmentMask>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask7" DataField="InventoryID" ></px:PXSegmentMask>
					<px:PXLayoutRule GroupCaption="Information" runat="server" ID="CstPXLayoutRule15" StartGroup="True" ></px:PXLayoutRule>
					<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown18" DataField="ShipDestType" ></px:PXDropDown>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask37" DataField="BranchID" ></px:PXSegmentMask>
					<px:PXSelector runat="server" ID="CstPXSelector19" DataField="ShipToBAccountID" ></px:PXSelector>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask20" DataField="ShipToLocationID" ></px:PXSegmentMask>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask36" DataField="ShipToSiteID" ></px:PXSegmentMask>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" ></px:PXLayoutRule>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule38" StartGroup="True" GroupCaption="Current Information" ></px:PXLayoutRule>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask40" DataField="SiteID" ></px:PXSegmentMask>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask39" DataField="LocationID" ></px:PXSegmentMask>
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule32" StartGroup="True" GroupCaption="Listing Information" ></px:PXLayoutRule>
					<px:PXDropDown runat="server" ID="CstPXDropDown35" DataField="Listing" ></px:PXDropDown>
					<px:PXTextEdit runat="server" ID="CstPXTextEdit30" DataField="ListingID" ></px:PXTextEdit>
					<px:PXLinkEdit runat="server" ID="CstPXTextEdit31" DataField="ListingURL" />
					<px:PXLayoutRule runat="server" ID="CstPXLayoutRule14" StartColumn="True" ></px:PXLayoutRule></Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Attributes">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
					<px:PXGrid runat="server" ID="PXGridAnswers" Height="150px" SkinID="Attributes" Width="420px" Caption="Attributes" MatrixMode="True" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="Answers" DataKeyNames="AttributeID,EntityType,EntityID">
								<RowTemplate>
									<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
									<px:PXTextEdit runat="server" ID="edParameterID" Enabled="False" DataField="AttributeID" />
									<px:PXTextEdit runat="server" ID="edAnswerValue" DataField="Value" /></RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="AttributeID" TextAlign="Left" TextField="AttributeID_description" Width="135px" AllowShowHide="False" />
									<px:PXGridColumn DataField="isRequired" Type="CheckBox" TextAlign="Center" Width="80px" />
									<px:PXGridColumn DataField="AttributeCategory" Type="DropDownList" />
									<px:PXGridColumn DataField="Value" Width="185px" /></Columns></px:PXGridLevel></Levels></px:PXGrid></Template></px:PXTabItem></Items>
            </px:PXTab>
</asp:Content>
