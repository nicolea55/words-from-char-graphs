using System;
using System.Collections.Generic;

namespace Graphs
{
	public class Node
	{
		public List<Node> Neibours = new List<Node>();

		public char CharVal = new char();

		public Node(char c)
		{
			CharVal = c;
		}
	}

	public class Graph
	{
		public List<Node> Nodes = new List<Node>();

		public void AddNode(Node node)
		{
			if (!Nodes.Contains (node)) {
				Nodes.Add (node);
			}
		}

		public void AddEdge( Node node1, Node node2)
		{
			if (!node1.Neibours.Contains (node2)) {
				node1.Neibours.Add (node2);
			}

			if (!node2.Neibours.Contains (node1)) {
				node2.Neibours.Add (node1);
			}
		}

		public void RemoveEdge(Node node1, Node node2)
		{
			if (node1.Neibours.Contains (node2)) {
				node1.Neibours.Remove (node2);
			}

			if (node2.Neibours.Contains (node1)) {
				node2.Neibours.Remove (node1);
			}
		}

		public bool IsEdge(Node node1, Node node2)
		{
			bool result = node1.Neibours.Contains (node2) && node2.Neibours.Contains (node1);
			return result;
		}

		public List<string> GetCombinations()
		{
			return MakeWords (Nodes);
		}

		// Current path
		private List<Node> currentPath = null;

		/// <summary>
		/// Makes letter comginations out of some nodes.
		/// </summary>
		/// <returns>The words.</returns>
		/// <param name="nodes">Nodes.</param>
		/// <remarks>
		/// This implementation is based on the assumption that each node is represented by a distinct Node object,
		/// and the nodes already a part of a graph, with all edges being defined.
		/// </remarks>
		private List<string> MakeWords(List<Node> nodes)
		{
			List<string> words = new List<string> ();

			bool iAmRoot = false;
			// If this method is called first time, it is root of tree
			if (currentPath == null) {
				iAmRoot = true;
			}

			// For each node get the tree
			foreach (Node node in nodes) {
				// When traversing the first time in recursion path,
				// each time the node is root, so reinit the checkedNodes list
				if (iAmRoot) {
					currentPath = new List<Node> ();
				}

				// If we did not met yet this node in current recursion path
				if(!currentPath.Contains(node))
				{
					currentPath.Add (node);
					string word = node.CharVal.ToString();
					words.Add (word);
					// for each tree path get the words list
					// check each word
					// if is word add it to list
					// return print the list
					if (node.Neibours != null) {
						List<string> otherWords = MakeWords (node.Neibours);
						foreach (string otherWord in otherWords) {
							string newWord = word + otherWord;
							words.Add (newWord);
						}
					}

					// Remove the node we checked to allow other paths to it
					if (currentPath.Contains (node)) {
						currentPath.Remove(node);
					}
				}
			}

			// Set recycle current path so that other call knows it is starting over
			if (iAmRoot) {
				currentPath = null;
			}

			return words;
		}
	}


}

