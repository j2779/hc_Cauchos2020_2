<%@ Page Title="" Language="C#" MasterPageFile="~/View/administrador/Admin.master" AutoEventWireup="true" CodeFile="~/Controller/administrador/Historial_ventas.aspx.cs" Inherits="View_administrador_Historial_ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <h1 class="text-center text-primary"><strong>Historial De Ventas
        <br />
        <small>Busque por fechas y empleados</small></strong></h1>
    <div class="col-sm-12">
        <div class="form-inline text-center">
            <div class="form-group">
                <asp:TextBox ID="TB_Dia" runat="server" CssClass="form-control" placeholder="Dia (DD)" TextMode="Number" Width="150px"></asp:TextBox>
                <asp:TextBox ID="TB_Mes" runat="server" CssClass="form-control" placeholder="Mes (MM)" TextMode="Number" Width="150px"></asp:TextBox>
                <asp:TextBox ID="TB_Ano" runat="server" CssClass="form-control" placeholder="Año (YYYY)" TextMode="Number" Width="150px"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <div class="text-center">
            <div class="form-group">
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TB_Dia" ErrorMessage="Dia invalido,Rango entre 1-31" MaximumValue="31" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TB_Mes" ErrorMessage="Mes invalido,Rango entre 1-12" MaximumValue="12" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="TB_Ano" ErrorMessage="Año invalido,Rango entre 2000-2100" MaximumValue="2100" MinimumValue="2000" Type="Integer"></asp:RangeValidator>
            </div>
        </div>
    </div>
    <div class="">
        <div class="form-inline text-center">
            <div class="form-group">
                <asp:RadioButtonList ID="RBL" runat="server" AutoPostBack="True" CssClass="form-group" OnSelectedIndexChanged="RBL_SelectedIndexChanged" Width="227px">
                    <asp:ListItem Value="0">Activar Empleado</asp:ListItem>
                    <asp:ListItem Value="1">Desactivar Empleado</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div class="col-sm-11">
        <div class="form-inline text-center">
            <div class="form-group">
                <asp:DropDownList ID="DDL_Empleado" runat="server" class="form-control" DataSourceID="ODS_Empleado" DataTextField="Nombre" DataValueField="User_id" Enabled="False" OnSelectedIndexChanged="DDL_Empleado_SelectedIndexChanged" Visible="False" Width="128px">
                    <asp:ListItem Value="0"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="Btn_Buscar" runat="server" CssClass="btn btn-primary" OnClick="Btn_Buscar_Click" Text="Buscar" />
                <asp:Button ID="Btn_Todos" runat="server" CssClass="btn btn-primary" OnClick="Btn_Todos_Click" style="margin-left: 0" Text="Todos" />
            </div>
        </div>
    </div>
    <asp:TextBox ID="TB_Aux" runat="server" TextMode="Number" Visible="False"></asp:TextBox>
    <asp:ObjectDataSource ID="ODS_Empleado" runat="server" SelectMethod="ConsultarEmpleado" TypeName="DAOAdmin"></asp:ObjectDataSource>
    <br />
    <br />
    <br />
    <div class="row">
        <div class=" col-lg-12 col-md-offset-0.5">
            <div>
                <br />
                <asp:GridView ID="GV_Historial" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ODS_Historial" ForeColor="#333333" GridLines="None" OnRowDataBound="GV_Historial_RowDataBound" ShowFooter="True" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" SortExpression="Empleado" />
                        <asp:BoundField DataField="Fecha_pedido" HeaderText="Fecha " SortExpression="Fecha_pedido" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="ODS_Historial" runat="server" SelectMethod="ConsultarVentas" TypeName="DAOAdmin"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialDia" runat="server" SelectMethod="ConsultarVentasDia" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Dia" Name="Dia" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialMes" runat="server" SelectMethod="ConsultarVentasMes" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialAno" runat="server" SelectMethod="ConsultarVentasAno" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialAnoDia" runat="server" SelectMethod="ConsultarVentasAnoDia" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Dia" Name="dia" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialAnoMesDia" runat="server" SelectMethod="ConsultarVentasAnoMesDia" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Dia" Name="dia" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialMesDia" runat="server" SelectMethod="ConsultarVentasMesDia" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Dia" Name="dia" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialEmpleado" runat="server" SelectMethod="ConsultarVentasAnoMesDiaEmpleado" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Dia" Name="dia" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="DDL_Empleado" Name="emp" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_HistorialAnoMesDia0" runat="server" SelectMethod="ConsultarVentasAnoMesDia" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Dia" Name="dia" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_1" runat="server" SelectMethod="ConsultarVentasAnMes" TypeName="DAOAdmin">
        <SelectParameters>
            <asp:ControlParameter ControlID="TB_Ano" Name="ano" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="TB_Mes" Name="mes" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>

