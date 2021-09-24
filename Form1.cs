using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factura
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod;
            string nom;
            float precio;

            cod = cmbProducto.SelectedIndex;
            nom = cmbProducto.SelectedItem.ToString();
            precio = cmbProducto.SelectedIndex;

            switch (cod)
            {
                case 0: lblCodigo.Text = "0001";break;
                case 1: lblCodigo.Text = "0002";break;
                case 2: lblCodigo.Text = "0003"; break;
                case 3: lblCodigo.Text = "0004"; break;
                case 4: lblCodigo.Text = "0005"; break;
                case 5: lblCodigo.Text = "0006"; break;
                case 6: lblCodigo.Text = "0007"; break;
                case 7: lblCodigo.Text = "0008"; break;
                case 8: lblCodigo.Text = "0009"; break;
                case 9: lblCodigo.Text = "0010"; break;
                case 10: lblCodigo.Text = "0011"; break;
                case 11: lblCodigo.Text = "0012"; break;
                case 12: lblCodigo.Text = "0013"; break;
                case 13: lblCodigo.Text = "0014"; break;
                case 14: lblCodigo.Text = "0015"; break;
                default: lblCodigo.Text = "00016";break;

            }

            switch (nom)
            {
                case "Buso": lblNombre.Text = "Buso";break;
                case "Gorra": lblNombre.Text = "Gorra";break;
                case "Camisa": lblNombre.Text = "Camisa"; break;
                case "Camiseta": lblNombre.Text = "Camiseta"; break;
                case "Pantalón": lblNombre.Text = "Pantalón"; break;
                case "Medias": lblNombre.Text = "Medias"; break;
                case "Ropa Interior": lblNombre.Text = "Ropa Interior"; break;
                case "Dividí": lblNombre.Text = "Dividí"; break;
                case "Zapatos": lblNombre.Text = "Zapatos"; break;
                case "Pulsera": lblNombre.Text = "Pulsera"; break;
                case "Correa": lblNombre.Text = "Correa"; break;
                case "Gorro": lblNombre.Text = "Gorro"; break;
                case "Guantes": lblNombre.Text = "Guantes"; break;
                case "Calentador": lblNombre.Text = "Calentador"; break;
                case "Chompa": lblNombre.Text = "Chompa"; break;
                default: lblNombre.Text = "Pijama";break;
            }

            switch (precio)
            {
                case 0: lblPrecio.Text = "050";break;
                case 1: lblPrecio.Text = "005";break;
                case 2: lblPrecio.Text = "015"; break;
                case 3: lblPrecio.Text = "020"; break;
                case 4: lblPrecio.Text = "025"; break;
                case 5: lblPrecio.Text = "002"; break;
                case 6: lblPrecio.Text = "010"; break;
                case 7: lblPrecio.Text = "020"; break;
                case 8: lblPrecio.Text = "040"; break;
                case 9: lblPrecio.Text = "002"; break;
                case 10: lblPrecio.Text = "005"; break;
                case 11: lblPrecio.Text = "003"; break;
                case 12: lblPrecio.Text = "010"; break;
                case 13: lblPrecio.Text = "050"; break;
                case 14: lblPrecio.Text = "030"; break;
                default: lblPrecio.Text = "060";break;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataGridViewRow file = new DataGridViewRow();
            file.CreateCells(dgvLista);

            file.Cells[0].Value = lblCodigo.Text;
            file.Cells[1].Value = lblNombre.Text;
            file.Cells[2].Value = lblPrecio.Text;
            file.Cells[3].Value = txtCantidad.Text;
            file.Cells[4].Value = (float.Parse(lblPrecio.Text) * float.Parse(txtCantidad.Text)).ToString();

            dgvLista.Rows.Add(file);

            lblCodigo.Text = lblNombre.Text = lblPrecio.Text = txtCantidad.Text = "";

            obtenerTotal();


        }

        public void obtenerTotal()
        {
            float costot = 0;
            int contador = 0;

            contador = dgvLista.RowCount;

            for (int i = 0; i < contador; i++)
           {
                costot += float.Parse(dgvLista.Rows[i].Cells[4].Value.ToString());
            }

            lblTotatlPagar.Text = costot.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try {
                DialogResult rppta = MessageBox.Show("¿Desea eliminar producto?",
                    "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rppta == DialogResult.Yes)
                {
                    dgvLista.Rows.Remove(dgvLista.CurrentRow);
                }
            }
            catch { }
            obtenerTotal();
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try {
                lblDevolucion.Text = (float.Parse(txtEfectivo.Text) - float.Parse(lblTotatlPagar.Text)).ToString();

                
            }
            catch { }

            if (txtEfectivo.Text == "")
            {
                lblDevolucion.Text = "";
            }

        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            clsFactura.CreaTicket Ticket1 = new clsFactura.CreaTicket();

            Ticket1.TextoCentro("Empresa de Ropa "); //Imprime una linea de descripcion
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //Imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: 0000001");
            Ticket1.TextoIzquierda("Fecha: " + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            Ticket1.TextoIzquierda("Le Atendio: KC&BC&MY");
            Ticket1.TextoIzquierda("");
            clsFactura.CreaTicket.LineasGuion();

            clsFactura.CreaTicket.EncabezadoVenta();
           clsFactura.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dgvLista.Rows)
            {
                // PROD                     //PRECIO                                    CANT                         TOTAL
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            clsFactura.CreaTicket.LineasGuion();
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(lblTotatlPagar.Text)); // Imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado: ", double.Parse(txtEfectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto: ", double.Parse(lblDevolucion.Text));


            // Ticket1.LineasTotales(); // imprime linea 

            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            Ticket1.ImprimirTiket(impresora);

            MessageBox.Show("Gracias por preferirnos");
            MessageBox.Show("Vuelva Pronto");

            this.Close();
        }


    }
}
