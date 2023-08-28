import json
import matplotlib.pyplot as plt

# Load JSON data from file
with open('data.json', 'r') as json_file:
    data = json.load(json_file)

# Create a dictionary to store nodes and their connections
graph = {}
for node in data['nodes']:
    graph[node['id']] = []

for edge in data['edges']:
    source, target = edge['source'], edge['target']
    graph[source].append(target)

# Visualization: A basic representation of the graph
def visualize_graph(graph):
    plt.figure(figsize=(8, 6))
    for node, neighbors in graph.items():
        for neighbor in neighbors:
            plt.plot([node, neighbor], [0, 1], marker='o', color='b', linewidth=1, markersize=8)
    plt.yticks([])
    plt.xlabel("Nodes")
    plt.title("Graph Visualization")
    plt.show()

visualize_graph(graph)
