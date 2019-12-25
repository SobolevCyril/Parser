using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Roslesinforg.Sigma.SrzParser;
using Antlr4.StringTemplate;

namespace SpeakerApp
{

    class Program
    {

        private static void printPrettyLispTree(String tree)
        {
            int indentation = 1;
            foreach (char c in tree.ToCharArray())
            {
                if (c == '(')
                {
                    if (indentation > 1)
                    {
                        Console.WriteLine("");
                    }

                    for (int i = 0; i < indentation; i++)
                    {
                        Console.Write("  ");
                    }

                    indentation++;
                }
                else if (c == ')')
                {
                    indentation--;
                }

                Console.Write(c);
            }

            Console.WriteLine();
        }

        private static void PrintDebugInfo(Lexer lexer)
        {
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            tokens.Fill();
            Console.WriteLine("\n[TOKENS]");
            foreach (var t in tokens.GetTokens())
            {

                String symbolicName = lexer.Vocabulary.GetSymbolicName(t.Type);
                String literalName = lexer.Vocabulary.GetLiteralName(t.Type);

                Console.WriteLine(" {0:-20} {1}", symbolicName == "" ? literalName : symbolicName,
                    t.Text.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t"));
            }

            Console.WriteLine("\n[PARSE-TREE]");
            var parser = new SRZParser(tokens);
            ParserRuleContext context = parser.expression();
            String tree = context.ToStringTree(parser);
            printPrettyLispTree(tree);
        }

        private static void Main(string[] args)
        {
            try
            {
                string input = "";
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {
                    ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing");
                    Console.WriteLine(e.Message);
                    return;
                }

                writer.AutoFlush = true;
                //Console.SetOut(writer);
                StringBuilder text = new StringBuilder();
                input = File.ReadAllText(@"..\..\sample8.txt");
                //Console.WriteLine("Input the validation rule.");


                AntlrInputStream inputStream = new AntlrInputStream(input);
                SRZLexer lexer = new SRZLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                // Внимание, отладка мешает запускать построение дерева
                //Program.PrintDebugInfo(lexer);
                //

                var parser = new SRZParser(commonTokenStream);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ErrorListener()); // add ours
                var tree = parser.start();
                var evalVisitor = new Expression();
                // результатом вычисления логического выражения будет true | false
                Console.WriteLine(evalVisitor.Visit(tree));

                Console.ReadKey();
            }
            catch (ParseCancellationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.ReadKey();
            }

        }
    }
} 


    
    

