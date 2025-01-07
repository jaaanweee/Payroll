using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using project.Data.Models.Domain;
using project.Data.Repository;

namespace project.UI.Controllers
{
    public class AllowanceController : Controller
    {

        private readonly IAllowanceRepository _allowanceRepo;
        public AllowanceController(IAllowanceRepository allowanceRepo)
        {
            _allowanceRepo = allowanceRepo;
        }
    
     
        public  async Task<IActionResult> Add() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Allowance allowance)
        {
            try
            {   if(!ModelState.IsValid)
                    return View(allowance);
                bool addAllowanceResult= await _allowanceRepo.AddAsync(allowance);
                if(addAllowanceResult)
                    TempData["msg"] = "successfully added";
                else
                    TempData["msg"] = "could not be added";

            }
            catch(Exception ex)
            {
                TempData["msg"] = "could not be added";
            }
            return RedirectToAction(nameof(Add));
            
        }

        public async Task<IActionResult> Edit(int id)
        { 
            var allowance= await _allowanceRepo.GetByIdAsync(id);
            return View(allowance);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Allowance allowance)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(allowance);
                var updateAllowanceResult = await _allowanceRepo.UpdateAsync(allowance);
                if (updateAllowanceResult)
                    TempData["msg"] = "successfully updated";
                else
                    TempData["msg"] = "could not be updtaed";
                    
                
            }
            catch (Exception ex)
            {
                TempData["msg"] = "could not be updtaed";
                
            }
            return View(allowance);

        }

       // public async Task<IActionResult> GetById(int id)
        //{
          //  return View();
        //}
        public async Task<IActionResult> DisplayAll()
        {   
            var allowances=await _allowanceRepo.GetAllAsync();
            return View(allowances);
        }
        public async Task<IActionResult> Delete(int id)
        {
        var deleteResult = await _allowanceRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }
    }
}
