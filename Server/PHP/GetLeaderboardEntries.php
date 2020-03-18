<?php
    header("Content-type: application/json; charset: utf-8");
    
    $con = mysqli_connect("localhost", "id12968469_tntlad", "Games@DHBW_000webhost", "id12968469_tntlad_db");
    mysqli_set_charset($con, "utf8");
    $con->set_charset("utf8");
    
    $top = $_GET["top"];

    $statement = mysqli_prepare($con, 
        "SELECT player_name, rounds_won, minutes_played FROM leaderboard WHERE 1 LIMIT ?"
    );
    mysqli_stmt_bind_param($statement, "i", $top);

    $response = array();
    if(mysqli_stmt_execute($statement))
    {
        mysqli_stmt_store_result($statement);
        mysqli_stmt_bind_result($statement, $player_name, $rounds_won, $minutes_played);
        
        $response["success"] = true;    
        while($statement->fetch())
        {
            $bindResults = array($player_name, $rounds_won, $minutes_played);
            array_push($response, $bindResults);
        }
    }
    else
    {
        $response["success"] = false;
    }
    
    echo json_encode($response);
?>