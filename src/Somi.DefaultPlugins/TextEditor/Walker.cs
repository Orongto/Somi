using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Somi.DefaultPlugins
{
    public class Walker : CSharpSyntaxWalker
    {
        public Walker() : base(SyntaxWalkerDepth.Token)
        {
        }

        public override void Visit(SyntaxNode? node)
        {
            base.Visit(node);
        }

        public override void VisitToken(SyntaxToken token)
        {
            switch (token.Kind())
            {
                case SyntaxKind.UsingKeyword:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case SyntaxKind.TypeKeyword:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case SyntaxKind.NamespaceKeyword:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case SyntaxKind.StaticKeyword:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case SyntaxKind.ClassKeyword:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case SyntaxKind.IdentifierToken:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case SyntaxKind.InterpolatedStringText:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case SyntaxKind.NewKeyword:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case SyntaxKind.StringKeyword:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case SyntaxKind.VoidKeyword:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case SyntaxKind.ArrayType:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write(token.SyntaxTree.GetText().GetSubText(token.FullSpan));
            base.VisitToken(token);
        }
    }
}