using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitBasket.Data;
using FruitBasket.Models;
using System.Collections.ObjectModel;

namespace FruitBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitBasketController : ControllerBase
    {
        private readonly FruitBasketContext _context;

        public FruitBasketController(FruitBasketContext context)
        {
            _context = context;
        }

        // GET: api/FruitBasket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Basket>>> GetBasket()
        {
            return await _context.Basket.ToListAsync();
        }

        // GET: api/FruitBasket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(int id)
        {
            var basket = await _context.Basket.FindAsync(id);

            if (basket == null)
            {
                return NotFound();
            }

            return basket;
        }

        // PUT: api/FruitBasket/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(int id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest();
            }

            _context.Entry(basket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FruitBasket
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
                var apples = new Fruit()
                {
                    Name = "Apple",
                    Color = "Red",
                    Amount = 3,
                    Weight = 4.5
                };
                var mangoes = new Fruit()
                {
                    Name = "Mango",
                    Color = "Yellow",
                    Amount = 20,
                    Weight = 12
                };
                var pears = new Fruit()
                {
                    Name = "Pear",
                    Color = "Green",
                    Amount = 8,
                    Weight = 3.2
                };
                var kiwies = new Fruit()
                {
                    Name = "Kiwies",
                    Color = "Brown",
                    Amount = 3,
                    Weight = 4.5
                };
                var oranges = new Fruit()
                {
                    Name = "Orange",
                    Color = "Orange",
                    Amount = 24,
                    Weight = 15
                };

                
                /*ctx.Fruit.Add(apples);
                ctx.Fruit.Add(mangoes);
                ctx.Fruit.Add(kiwies);
                ctx.Fruit.Add(pears);
                ctx.Fruit.Add(oranges);*/

            
                basket = new Basket()
                {
                    AvailableFruitSpace = 100 - (apples.Amount + mangoes.Amount + pears.Amount + kiwies.Amount + oranges.Amount)
                };

                basket.Fruit = new ObservableCollection<Fruit>();

                basket.Fruit.Add(apples);
                basket.Fruit.Add(mangoes);
                basket.Fruit.Add(pears);
                basket.Fruit.Add(kiwies);
                basket.Fruit.Add(oranges);
                
                _context.Basket.Add(basket);
                await _context.SaveChangesAsync();

            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        // DELETE: api/FruitBasket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var basket = await _context.Basket.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            _context.Basket.Remove(basket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BasketExists(int id)
        {
            return _context.Basket.Any(e => e.Id == id);
        }
    }
}
