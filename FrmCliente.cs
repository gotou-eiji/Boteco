using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boteco
{
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Por favor, digite um ID!");
                this.txtId.Focus();
            }
            else
            {
                int Id = Convert.ToInt32(txtId.Text.Trim());
                Cliente cliente = new Cliente();
                cliente.Localizar(Id);
                txtNome.Text = cliente.nome;
                txtCpf.Text = cliente.cpf;
                txtDataNascimento.Text = cliente.data_nascimento;
                txtCelular.Text = cliente.celular;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "" && txtCpf.Text == "" && txtCelular.Text == "")
                {
                    MessageBox.Show("Por favor, preencha o formulário para inserir.");
                    this.txtNome.Focus();
                }
                else
                {
                    Cliente cliente = new Cliente();
                    if (cliente.RegistroRepetido(txtNome.Text, txtCpf.Text, txtCelular.Text) != false)
                    {
                        MessageBox.Show("Este cliente já está em nossa base de dados!");
                        List<Cliente> clientes = cliente.listacliente();
                        dgvCliente.DataSource = cliente;
                        txtNome.Text = "";
                        txtCpf.Text = "";
                        txtDataNascimento.Text = "";
                        txtCelular.Text = "";
                        this.txtNome.Focus();
                    }
                    else
                    {
                        cliente.Cadastrar(txtNome.Text, txtCpf.Text, txtDataNascimento.Text, txtCelular.Text);
                        MessageBox.Show("Cliente cadastrado com sucesso!");
                        List<Cliente> clientes = cliente.listacliente();
                        dgvCliente.DataSource = clientes;
                        txtNome.Text = "";
                        txtDataNascimento.Text = "";
                        txtCelular.Text = "";
                        this.txtNome.Focus();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
            Cliente cliente = new Cliente();
            cliente.Editar(Id, txtNome.Text, txtCpf.Text, txtDataNascimento.Text, txtCelular.Text);
            MessageBox.Show("Cliente atualizado com sucesso!");
            List<Cliente> clientes = cliente.listacliente();
            dgvCliente.DataSource = clientes;
            txtId.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
            txtDataNascimento.Text = "";
            txtCelular.Text = "";
            this.txtNome.Focus();
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
            Cliente cliente = new Cliente();
            cliente.Excluir(Id);
            MessageBox.Show("Cliente excluído com sucesso!");
            List<Cliente> clientes = cliente.listacliente();
            dgvCliente.DataSource = clientes;
            txtId.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
            txtDataNascimento.Text = "";
            txtCelular.Text = "";
            this.txtNome.Focus();
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Deseja realmente sair", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvCliente.Rows[e.RowIndex];
                row.Selected = true;
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCpf.Text = row.Cells[2].Value.ToString();
                txtDataNascimento.Text = row.Cells[3].Value.ToString();
                txtCelular.Text = row.Cells[4].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void FrmCliente_Load_1(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            List<Cliente> clientes = cliente.listacliente();
            dgvCliente.DataSource = clientes;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }
    }
}
