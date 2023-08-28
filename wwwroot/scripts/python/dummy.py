import json
import matplotlib.pyplot as plt
import networkx as nx

# Load JSON data from file
with open('data.json', 'r') as json_file:
    data = json.load(json_file)

# Create a directed graph using NetworkX
G = nx.DiGraph()

# Add nodes to the graph
for node in data['nodes']:
    G.add_node(node['id'], label=node['label'])

# Add edges to the graph
for edge in data['edges']:
    G.add_edge(edge['source'], edge['target'])

# Draw the graph
pos = nx.spring_layout(G)  # Layout algorithm
labels = nx.get_node_attributes(G, 'label')
nx.draw(G, pos, with_labels=True, labels=labels, node_size=2000, node_color='skyblue', font_size=10, font_color='black')
plt.show()
