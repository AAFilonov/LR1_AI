package game;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.Set;
import java.util.TreeSet;

class BoardConstructorTest
{
    @Test
    public void testConstruct_oneNode()
    {
        BoardConstructor boardConstructor = new BoardConstructor();

        Board actualBoard = boardConstructor.construct(1);
        Assertions.assertEquals("Board(Root=Node{id=0, state=EMPTY, siblings=[[node 1], [node 2], [node 3], [node 4], [node 5], [node 6]]})"
                , actualBoard.toString());

        System.out.println(actualBoard.Root);
        for (Node sibling : actualBoard.Root.getSiblings())
        {
            System.out.println(sibling);
        }
    }

    @Test
    public void testConstruct_oneLayer()
    {
        BoardConstructor boardConstructor = new BoardConstructor();

        Board actualBoard = boardConstructor.construct(1);

        StringBuilder sb = new StringBuilder();
        sb.append(actualBoard.Root + "\n");
        for (Node sibling : actualBoard.Root.getSiblings())
        {
            sb.append(sibling + "\n");
        }

        System.out.println(sb);
        Assertions.assertEquals("Node{id=0, state=EMPTY, siblings=[[node 1], [node 2], [node 3], [node 4], [node 5], [node 6]]}\n" +
                        "Node{id=1, state=EMPTY, siblings=[[node 0], [node 2], [node 6]]}\n" +
                        "Node{id=2, state=EMPTY, siblings=[[node 1], [node 0], [node 3]]}\n" +
                        "Node{id=3, state=EMPTY, siblings=[[node 2], [node 0], [node 4]]}\n" +
                        "Node{id=4, state=EMPTY, siblings=[[node 3], [node 0], [node 5]]}\n" +
                        "Node{id=5, state=EMPTY, siblings=[[node 4], [node 0], [node 6]]}\n" +
                        "Node{id=6, state=EMPTY, siblings=[[node 1], [node 5], [node 0]]}\n"
                , sb.toString());

        System.out.println();


    }

    @Test
    public void testConstruct_twoLayers()
    {
        BoardConstructor boardConstructor = new BoardConstructor();

        Board actualBoard = boardConstructor.construct(2);

        StringBuilder sb = new StringBuilder();



        var allNodes = getAllNodes(actualBoard.Root);
        for (Node sibling : allNodes)
        {
            sb.append(sibling + "\n");
        }

        System.out.println(sb);

        Assertions.assertEquals(""
                , sb.toString());


    }

    Set<Node> getAllNodes(Node root)
    {
        Set<Node> nodes = new TreeSet<>();

        getSiblings(root, nodes);
        return nodes;
    }

    Set<Node> getSiblings(Node node, Set<Node> nodes)
    {

        nodes.add(node);
        for (Node sibling : node.getSiblings())
        {
            if (!nodes.contains(sibling))
                nodes.addAll(getSiblings(sibling,nodes));
        }
        return nodes;
    }

}