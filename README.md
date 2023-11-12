# GraphsWeb

Web application for finding the shortest path in weighted graph using Dijkstra's algorithm.

## Prerequisites

* Modern browser which supports WebAssembly [see](https://developer.mozilla.org/en-US/docs/WebAssembly#browser_compatibility)
* .NET 7 Runtime - [*(download)*](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-7.0.13-windows-hosting-bundle-installer)

## Used Technologies

* Git - project is stored on [GitHub](https://github.com/JiriHotovec/GraphsWeb)
* .NET 7
* WebAssembly
* File storage
* Linq to Objects
* Immutable Value Types
* Code covered by Unit Tests
* Recursion call
* Components implement interfaces
* Dijkstra's algorithm
* Weighted graph - current version includes only undirected graph implementation (A, B) = (B, A)

## Algorithm

### Dijkstra's Algorithm

Dijkstra's algorithm is used to find shortest path through the graph.

## Storage

Graph is stored as *.json file.

### IGraphStorage Interface

New storage could be implemented by interface *IGraphStorage*.

### Json File Structure

```json
{
    "Name": "Weighted Graph Example",
    "Edges": [
        {
            "Weight": {
                "Value": 1
            },
            "Source": {
                "Name": "A"
            },
            "Destination": {
                "Name": "B"
            }
        }
    ]
}
```