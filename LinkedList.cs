#nullable enable
using System;
using static System.Console;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Method used to indicate a target Drug object when searching.
        
        public static bool IsTarget( Drug data ) 
        { 
            return data.Name.StartsWith( "FOSAMAX", StringComparison.OrdinalIgnoreCase ); 
        }
        
        // Method used to compare two Drug objects when sorting.
        // Return is -/0/+ for a<b/a=b/a>b, respectively.
        
        public static int Compare( Drug a, Drug b )
        {
            return string.Compare( a.Name, b.Name, StringComparison.OrdinalIgnoreCase );
        }
        
        // Method used to add a new Drug object to the linked list in sorted order.
        
        public void InsertInOrder( Drug newData )
        {
            if( newData == null ) throw new ArgumentNullException( nameof( newData ) );
            
            Node newNode = new Node( newData );
            Node? curNode = Head!;
            Node? prevNode = null;
            
            if( Head == null )
            {
                Head = newNode;
                Tail = newNode;
                Count++;
                return;
            }
            
            for( int i = 0; i < Count; i++ )
            {
                if( Compare( curNode!.Data, newNode.Data ) > 0 )
                {
                    if( curNode == Head )
                    {
                        newNode.Next = Head;
                        Head = newNode;
                        Count++;
                        return;
                    }
                    else
                    {
                        prevNode!.Next = newNode;
                        newNode.Next = curNode;
                        Count++;
                        return;
                    }
                }
                prevNode = curNode;
                curNode = curNode.Next;
            }
            Tail!.Next = newNode;
            Tail = newNode;
            Count++;
        }
    }
    
    static class Program
    {
        // Method to test operation of the linked list.
        
        static void Main( )
        {
            Drug[ ] drugArray = Drug.ArrayFromFile( "RXQT1503-100.txt" );
            
            LinkedList drugList = new LinkedList( );
            foreach( Drug d in drugArray ) drugList.InsertInOrder( d );
            
            WriteLine( "drugList.Count = {0}", drugList.Count );
            foreach( Drug d in drugList.ToArray( ) ) WriteLine( d );
        }
    }
}
