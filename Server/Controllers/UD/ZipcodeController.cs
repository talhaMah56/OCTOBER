﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;
using static Duende.IdentityServer.Models.IdentityResources;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]

    public class ZipcodeController : BaseController, GenericRestController<ZipcodeDTO>
    {
        public ZipcodeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{Zip}")]

        public async Task<IActionResult> Delete(string Zip)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes.Where(x => x.Zip.Equals(Zip))
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Zipcodes.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]

        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Zipcodes.Select(sp => new ZipcodeDTO
                {
                    Zip = sp.Zip,
                    City = sp.City,
                    State = sp.State,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{Zip}")]

        public async Task<IActionResult> Get(int Zip)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                ZipcodeDTO? result = await _context
                    .Zipcodes
                    .Where(x => x.Zip.Equals(Zip))
                    .Select(sp => new ZipcodeDTO
                    {
                        Zip = sp.Zip,
                        City = sp.City,
                        State = sp.State,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                    })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]

        public async Task<IActionResult> Post([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes
                    .Where(x => x.Zip.Equals(_ZipcodeDTO.Zip))
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Zipcode z = new Zipcode
                    {
                        Zip = _ZipcodeDTO.Zip,
                        City = _ZipcodeDTO.City,
                        State = _ZipcodeDTO.State,
                    };
                    _context.Zipcodes.Add(z);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]

        public async Task<IActionResult> Put([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes.Where(x => x.Zip == _ZipcodeDTO.Zip)
                    .FirstOrDefaultAsync();

                itm.Zip = _ZipcodeDTO.Zip;
                itm.City = _ZipcodeDTO.City;
                itm.State = _ZipcodeDTO.State;


                _context.Zipcodes.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        //Implemented the function with String parameter
        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

    }
}

