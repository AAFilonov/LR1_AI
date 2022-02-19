package game;

public class BoardConstructor
{
    int MAX_AMOUNT_OF_SIBLINGS = 6;
    int MAX_SIBLING_INDEX = 5;

    public Board construct(int size)
    {

        Node rootNode = createNodeWithSibling(size);
        return new Board(rootNode);
    }

    private Node createNodeWithSibling(int amount_of_layers)
    {
        Node rootNode = new Node();
        if (amount_of_layers == 0)
            return rootNode;
        //1 layer
        for (int i = 0; i < 6; i++)
        {
            Node siblingNode = new Node();
            switch (i)
            {
                case 0:
                    break;
                case 5:
                    bind(siblingNode, rootNode.siblings.get(0));
                default:
                    bind(siblingNode, rootNode.siblings.get(i - 1));

            }
            bind(rootNode, siblingNode);
        }
        if (amount_of_layers == 1)
            return rootNode;
        //2 layer
        for (int i = 0; i < 6; i++)
        {
            Node siblingNode = rootNode.siblings.get(i);
            switch (i)
            {
                case 0:
                {
                    Node externalNode1 = new Node();
                    Node externalNode2 = new Node();


                    bind(siblingNode, externalNode1);
                    bind(siblingNode, externalNode2);

                    bind(externalNode1, externalNode2);


                }
                break;
                case 1:
                case 2:
                case 3:
                case 4:

                case 5:
                {

                    Node previousRootSibling = rootNode.siblings.get(i - 1);

                    createAndLink2ExternalNodes(siblingNode, previousRootSibling);
                    Node firstRootSibling = rootNode.siblings.get(0);
                    bind( getLastSibling(siblingNode),firstRootSibling.getSiblings().get(4));

                }
                break;
                default:
                {
                    Node previousRootSibling = rootNode.siblings.get(i - 1);
                    createAndLink2ExternalNodes(siblingNode, previousRootSibling);


                }
            }
        }
        return rootNode;

    }

    private void createAndLink2ExternalNodes(Node siblingNode, Node previousRootSibling)
    {
        Node externalNode1 = getLastSibling(previousRootSibling);
        Node externalNode2 = new Node();
        Node externalNode3 = new Node();


        bind(siblingNode, externalNode1);
        bind(siblingNode, externalNode2);
        bind(siblingNode, externalNode3);


        bind(externalNode1, externalNode2);
        bind(externalNode2, externalNode3);
    }

    private Node getLastSibling(Node node)
    {
        return node.getSiblings().get(node.getSiblings().size() - 1);
    }

    private void bind(Node leftNode, Node rightNode)
    {
        leftNode.siblings.add(rightNode);
        rightNode.siblings.add(leftNode);
    }
}
