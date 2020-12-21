using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD.Controllers
{
    public class CidadeController : Controller
    {
        // GET: CidadeController
        public IActionResult Index(int idUF, int idRegiaoUF, string nomeCidade)
        {
            CidadeDAL dal = new CidadeDAL();            
            List<Cidade> lstCidade;
            if (idUF != 0 || idRegiaoUF != 0 || !string.IsNullOrEmpty(nomeCidade))            
                lstCidade = dal.BuscarCidadesFiltro(idUF, idRegiaoUF, nomeCidade);            
            else            
                lstCidade = dal.BuscarTodasCidades();            

            CidadeSimples CidadeSimples = new CidadeSimples();
            CidadeSimples.CidadeModel = lstCidade;

            UFDAL UFdal = new UFDAL();
            CidadeSimples.UFs = new SelectList(UFdal.BuscarTodasUF(), "Id", "Nome");

            RegiaoUFDAL RegiaoUFDal = new RegiaoUFDAL();
            CidadeSimples.Regioes = new SelectList(RegiaoUFDal.BuscarRegioesUF(lstCidade[0].Iduf), "Id", "Nome");

            return View(CidadeSimples);
        }

        // GET: CidadeController/Create
        public IActionResult Create()
        {                          
            var cidade = new Cidade();

            UFDAL dal = new UFDAL();
            cidade.UFs = new SelectList(dal.BuscarTodasUF(), "Id", "Nome");        

            return View(cidade);
        }

        // POST: CidadeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cidade cidade)
        {
            try
            {
                CidadeDAL dal = new CidadeDAL();
                dal.InserirCidade(cidade);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: CidadeController/Edit/5
        public IActionResult Edit(int id)
        {
            CidadeDAL dal = new CidadeDAL();
            Cidade cidade = dal.BuscarCidadeId(id);            

            UFDAL UFDal = new UFDAL();
            cidade.UFs = new SelectList(UFDal.BuscarTodasUF(), "Id", "Nome",cidade.Iduf);
            
            RegiaoUFDAL RegiaoUFDal = new RegiaoUFDAL();
            cidade.Regioes = new SelectList(RegiaoUFDal.BuscarRegioesUF(cidade.Iduf), "Id", "Nome", cidade.Idregiaouf);

            return View(cidade);
        }

        // POST: CidadeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection fc)
        {
            try
            {
                var cidade = new Cidade();
                cidade.Id = Convert.ToInt32(fc["Id"]);
                cidade.Nome = fc["Nome"].ToString();
                cidade.Ibge = Convert.ToInt32(fc["Ibge"]);
                cidade.Iduf = Convert.ToInt32(fc["Iduf"][0]);
                cidade.Idregiaouf = Convert.ToInt32(fc["Idregiaouf"][0]);
                cidade.Latitude = fc["Latitude"].ToString();
                cidade.Longitude = fc["Longitude"].ToString();

                CidadeDAL dal = new CidadeDAL();
                dal.AlterarCidade(cidade);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: CidadeController/Delete/5
        public IActionResult Delete(int id)
        {
            CidadeDAL dal = new CidadeDAL();
            Cidade cidade = dal.BuscarCidadeId(id);
            return View(cidade);
        }

        // POST: CidadeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CidadeDAL dal = new CidadeDAL();
                dal.DeletarCidade(id);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
 
        public JsonResult OnGetRegioesUF(int IdUf)
        {
            RegiaoUFDAL RegialDAL = new RegiaoUFDAL();
            return new JsonResult(RegialDAL.BuscarRegioesUF(IdUf));
        }

        public IActionResult Relatorio(int area) //(int idUf, int idRegiaoUF, string nomeCidade)
        {
            CidadeDAL dal = new CidadeDAL();
            var lstCidade = dal.BuscarQtdeCidades(area).ToList();

            return View(lstCidade);
        }

    }
}
