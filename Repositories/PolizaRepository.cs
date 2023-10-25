using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoPolizas.Models;

namespace ProyectoPolizas.Repositories
{
    public class PolizaRepository
    {
        private readonly IMongoCollection<Poliza> _collection;

        public PolizaRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Poliza>("Polizas");
        }

        public List<Poliza> BuscarPorPlaca(string placa)
        {
            return _collection.Find(p => p.PlacaAutomotor == placa).ToList();
        }

        public List<Poliza> BuscarPorNumeroPoliza(int numeroPoliza)
        {
            return _collection.Find(p => p.NumeroPoliza == numeroPoliza).ToList();
        }

        public void InsertarPoliza(Poliza poliza)
        {
            _collection.InsertOne(poliza);
        }
    }
}