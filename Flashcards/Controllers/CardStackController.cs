using Flashcards.DTOs;
using Flashcards.Entities;
using Flashcards.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Flashcards.Controllers
{
    public class CardStackController
    {
        private readonly CardStackRepository repository;

        public CardStackController(CardStackRepository repository)
        {
            this.repository = repository;
        }

        public void CreateCardStack(CardStackDTO cardStack)
        {
            var cardEntity = new CardStackEntity
            {
                Name = cardStack.Name,
            };
            repository.CreateCardStack(cardEntity);
        }

        public IEnumerable<CardStackDTO> ReadCardStacks()
        {
            foreach (var cardStack in repository.ReadCardStacks())
                yield return new CardStackDTO { Id = cardStack.Id, Name = cardStack.Name };
        }

    }
}
