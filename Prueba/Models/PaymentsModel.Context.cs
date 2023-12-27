﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prueba.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class RECIPTSEntities1 : DbContext
    {
        public RECIPTSEntities1()
            : base("name=RECIPTSEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
    
        public virtual ObjectResult<GetUsers_Result> GetUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUsers_Result>("GetUsers");
        }
    
        public virtual ObjectResult<ValidateUser_Result> ValidateUser(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidateUser_Result>("ValidateUser", emailParameter, passwordParameter);
        }
    
        public virtual int AddPayment(Nullable<int> userID, string description, Nullable<int> amount, Nullable<System.DateTime> date)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddPayment", userIDParameter, descriptionParameter, amountParameter, dateParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetUserIDByEmail(string email)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetUserIDByEmail", emailParameter);
        }
    
        public virtual int UpdatePayment(Nullable<int> paymentID, string description, Nullable<decimal> amount, Nullable<System.DateTime> date)
        {
            var paymentIDParameter = paymentID.HasValue ?
                new ObjectParameter("PaymentID", paymentID) :
                new ObjectParameter("PaymentID", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(decimal));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdatePayment", paymentIDParameter, descriptionParameter, amountParameter, dateParameter);
        }
    
        public virtual ObjectResult<GetAllPayments_Result2> GetAllPayments()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllPayments_Result2>("GetAllPayments");
        }
    
        public virtual int AddUser(string name, string email, string password)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddUser", nameParameter, emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<GetPaymentsByUserID_Result1> GetPaymentsByUserID(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPaymentsByUserID_Result1>("GetPaymentsByUserID", userIDParameter);
        }
    
        public virtual int DeletePaymentByID(Nullable<int> paymentID)
        {
            var paymentIDParameter = paymentID.HasValue ?
                new ObjectParameter("PaymentID", paymentID) :
                new ObjectParameter("PaymentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeletePaymentByID", paymentIDParameter);
        }
    }
}
