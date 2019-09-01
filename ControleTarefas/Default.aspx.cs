using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleTarefas
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarLista();
        }

        private void CarregarLista()
        {
            TarefasDBEntities context = new TarefasDBEntities();
            List<TB_TAREFA> lstTarefa = context.TB_TAREFA.ToList<TB_TAREFA>();

            GVTarefa.DataSource = lstTarefa;
            GVTarefa.DataBind();
        }
    

        public void DisplayAlert(string titulo, string mensagem, System.Web.UI.Page page)
        {
            h1.InnerText = titulo;
            lblMsgPopup.InnerText = mensagem;
            ClientScript.RegisterStartupScript(typeof(Page), Guid.NewGuid().ToString(), "MostrarPopupMensagem();", true);
        }

        protected void GVTarefa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idItem = Convert.ToInt32(e.CommandArgument.ToString());
            TarefasDBEntities contextTarefa = new TarefasDBEntities();
            TB_TAREFA tarefa = new TB_TAREFA();

            tarefa = contextTarefa.TB_TAREFA.First(c => c.id == idItem);

            if (e.CommandName == "ALTERAR")
            {
                Response.Redirect("Tarefa.aspx?idItem=" + idItem);
            }
            else if (e.CommandName == "EXCLUIR")
            {
                contextTarefa.TB_TAREFA.Remove(tarefa);
                contextTarefa.SaveChanges();
                string msg = "Tarefa excluida com sucesso!";
                string titulo = "Informacao";
                CarregarLista();
                DisplayAlert(titulo, msg, this);
            }
        }
    }
}