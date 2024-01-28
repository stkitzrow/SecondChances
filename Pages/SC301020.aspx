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
            <px:PXSelector ID="edObjectID" runat="server" DataField="ObjectID" Size="s" TabIndex="101"></px:PXSelector>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule8" StartRow="True" ></px:PXLayoutRule>
	<px:PXTextEdit runat="server" ID="CstPXTextEdit5" DataField=".Descr" ></px:PXTextEdit>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="150px">
            </px:PXLayoutRule>
	<px:PXLayoutRule runat="server" ID="CstPXLayoutRule11" StartRow="True" />
	<px:PXDropDown runat="server" ID="CstPXDropDown12" DataField="CurrentDocument.Status" /></Template>
    </px:PXFormView>
	<px:PXTab runat="server" ID="MainTab">
		<Items>
			<px:PXTabItem Text="Pictures" >
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                    <px:PXImageUploader Height="320px" Width="430px" ID="imgUploader" runat="server" DataField="ImageUrl" AllowUpload="true" AllowNoImage="true" ShowComment="true" DataMember="CurrentDocument"
						 />
            </Template>
            </px:PXTabItem>
<%--			<px:PXTabItem Text="Detail Description" >
                <Template>
                    <px:PXRichTextEdit ID="edBody" runat="server" DataField="Body" Style="border-width: 0px; border-top-width: 1px; width: 100%;"
                        AllowAttached="true" AllowSearch="true" AllowLoadTemplate="false" AllowSourceMode="true">
                        <AutoSize Enabled="True" MinHeight="216" />
                    </px:PXRichTextEdit>
                </Template>
            </px:PXTabItem>
			<px:PXTabItem Text="General" >
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                    <px:PXDropDown runat="server" ID="CstPXDropDown12" DataField="CurrentDocument.Status" />
                </Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Attributes">
                <Template>
                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                    <px:PXGrid ID="PXGridAnswers" runat="server" Caption="Attributes" DataSourceID="ds" Height="150px" MatrixMode="True" Width="420px" SkinID="Attributes">
                        <Levels>
                            <px:PXGridLevel DataKeyNames="AttributeID,EntityType,EntityID" DataMember="Answers">
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" ControlSize="XM" LabelsWidth="M" StartColumn="True" />
                                    <px:PXTextEdit ID="edParameterID" runat="server" DataField="AttributeID" Enabled="False" />
                                    <px:PXTextEdit ID="edAnswerValue" runat="server" DataField="Value" />
                                </RowTemplate>
                                <Columns>
                                    <px:PXGridColumn AllowShowHide="False" DataField="AttributeID" TextField="AttributeID_description" TextAlign="Left" Width="135px" />
                                    <px:PXGridColumn DataField="isRequired" TextAlign="Center" Type="CheckBox" Width="80px" />
									<px:PXGridColumn DataField="AttributeCategory" Type="DropDownList" />
                                    <px:PXGridColumn DataField="Value" Width="185px" />
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>--%></Items></px:PXTab></asp:Content>
