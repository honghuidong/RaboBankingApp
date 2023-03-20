using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaboBankingApp.Dto;
using Transaction = RaboBankingApp.Entities.Transaction;

namespace RaboBankingApp.Controllers
{
    [ApiController]
    [Route("/api/rabobankapp")]
    public class TransactionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TransactionController(DataContext dataContext)
        {
            _dataContext  = dataContext;
        }

        [HttpGet]
        [Route("transactions")]
        public List<Transaction> GetTransactions()
        {
            var transactions = _dataContext.Transactions.ToList()
                               ?? throw new InvalidDataException("There are no transactions available");
            return transactions;
        }

        [HttpGet]
        [Route("transactions/{year:int}/{monthNumber:int}")]
        public List<Transaction> GetMonthlyTransactions(int year, int monthNumber)
        {
            var transactions = _dataContext.Transactions.ToList()
                               ?? throw new InvalidDataException("There are no transactions available");
            DateTime firstDayMonth = new DateTime(year, monthNumber, 1);
            DateTime firstDayNextMonth = new DateTime(year, monthNumber+1, 1);

            var monthlyTransactions = transactions.Where(t => t.Date >= firstDayMonth && t.Date < firstDayNextMonth).ToList();
            return monthlyTransactions;
        }

        [HttpPost]
        [Route("transaction")]
        public void PostTransaction([FromBody]Transaction transaction)
        {
            _dataContext.Transactions?.Add(transaction);
        }




        [HttpDelete]
        [Route("transaction/{id:int}")]
        public void DeleteTransaction(int id)
        {
            //try
            //{
            //    _dataContext.Transactions.Remove;
            //    var employeeToDelete = await employeeRepository.GetEmployee(id);

            //    if (employeeToDelete == null)
            //    {
            //        return NotFound($"Employee with Id = {id} not found");
            //    }

            //    return await employeeRepository.DeleteEmployee(id);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Error deleting data");
            //}


        }

    }
}
