// File: IAcceptVisitor.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 06 08 2015
namespace Domain.Interfaces
{
    public interface IAcceptVisitor
    {
        void Accept(IVisitor visitor);
    }
}