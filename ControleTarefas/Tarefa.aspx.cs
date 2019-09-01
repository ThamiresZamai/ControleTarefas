using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleTarefas
{
    public partial class Tarefa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDadosPagina();
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            string descricaoTarefa = txtDescricao.Text;
            DateTime dtAtividade = Convert.ToDateTime(txtdtAtividade.Text);
            DateTime dtEntrega = Convert.ToDateTime(txtdtEntrega.Text);
            DateTime dtLimite = Convert.ToDateTime(txtdtLimite.Text);
            TB_TAREFA t = new TB_TAREFA() { descricao = descricaoTarefa, dtAtividade = dtAtividade, dtEntrega = dtEntrega, dtLimiteEntrega = dtLimite };
            TarefasDBEntities contextTarefa = new TarefasDBEntities();

            string valor = Request.QueryString["idItem"];

            if (String.IsNullOrEmpty(valor))
            {
                contextTarefa.TB_TAREFA.Add(t);
                Diagnostico();
                Clear();
            }
            else
            {
                int id = Convert.ToInt32(valor);
                TB_TAREFA tarefa = contextTarefa.TB_TAREFA.First(c => c.id == id);
                tarefa.descricao = t.descricao;
                tarefa.dtAtividade = t.dtAtividade;
                tarefa.dtEntrega = t.dtEntrega;
                tarefa.dtLimiteEntrega = t.dtLimiteEntrega;
                lblmsg.Text = "Registro Alterado!";
            }

            contextTarefa.SaveChanges();
        }

        private void CarregarDadosPagina()
        {
            string valor = Request.QueryString["idItem"];
            int idItem = 0;
            TB_TAREFA tarefa = new TB_TAREFA();
            TarefasDBEntities contextTarefa = new TarefasDBEntities();

            if (!String.IsNullOrEmpty(valor))
            {
                idItem = Convert.ToInt32(valor);
                tarefa = contextTarefa.TB_TAREFA.First(c => c.id == idItem);

                txtDescricao.Text = tarefa.descricao;
                txtdtAtividade.Text = tarefa.dtAtividade.ToString();
                txtdtEntrega.Text = tarefa.dtEntrega.ToString();
                txtdtLimite.Text = tarefa.dtLimiteEntrega.ToString();

            }
        }

        private void Clear()
        {
            txtdtAtividade.Text = "";
            txtdtEntrega.Text = "";
            txtdtLimite.Text = "";
            txtDescricao.Text = "";
            txtDescricao.Focus();
        }

        private void Diagnostico() {
            
            DateTime dtEntrega = Convert.ToDateTime(txtdtEntrega.Text);
            DateTime dtLimite = Convert.ToDateTime(txtdtLimite.Text);

            if (dtEntrega > dtLimite)
            {
                lblmsg.Text = "Preciso focar algumas horas da semana para poder entregar minhas atividades";
            }
            else if (dtEntrega == dtLimite)
            {
                lblmsg.Text = "Estou dentro do prazo";
            }
            else if (dtEntrega < dtLimite) {
                lblmsg.Text = "Bruto!!";
            }
        }
    }
}