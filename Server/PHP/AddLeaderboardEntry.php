<?php
    header("Content-type: application/json; charset: utf-8");
    $con = mysqli_connect("localhost", "id12968469_tntlad", "Games@DHBW_000webhost", "id12968469_tntlad_db");
    mysqli_set_charset($con, "utf8");
    $con->set_charset("utf8");
    
    	
    $player_name = $_GET["player_name"];
    $rounds_won = $_GET["rounds_won"];
    $minutes_played = $_GET["minutes_played"];


    $statement = mysqli_prepare($con, "REPLACE INTO leaderboard (player_name, rounds_won, minutes_played) VALUES (?, ?, ?)");
    mysqli_stmt_bind_param($statement, "sii", $player_name, $rounds_won, $minutes_played);
    $response = array();
    
    if(mysqli_stmt_execute($statement)){
        $response["success"] = true;
    }else{
        $response["success"] = false;
    }        

    echo json_encode($response);
?>