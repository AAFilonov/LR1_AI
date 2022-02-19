package game;

import lombok.*;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@AllArgsConstructor
@Getter
@Setter
public class Node implements  Comparable<Node>
{
    static Integer MaxId = 0;
    Integer id;

    @Override
    public int compareTo(Node o)



    {
        return this.id-o.id;
    }


    private enum State
    {
        EMPTY,
        RED
    }

    Node(){
        this.id = MaxId;
        MaxId++;
    }

    State state =State.EMPTY;
    ArrayList<Node> siblings = new ArrayList<>();

    @Override
    public String toString()
    {
        List<String> siblingsStr = siblings.stream().map(node -> "[node "+node.id.toString()+"]").collect(Collectors.toList());
        return "Node{" +
                "id=" + id +
                ", state=" + state +
                ", siblings=" + siblingsStr +
                '}';
    }


}
