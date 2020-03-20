<?php
    header("Content-type: application/json; charset: utf-8");
    $response = array();
    
    $link = mysqli_connect("localhost", "id12968469_tntlad", "Games@DHBW_000webhost", "id12968469_tntlad_db");
    
    /* check connection */
    if (mysqli_connect_errno()) {
        $response["success"] = false;
        echo $response;
        exit();
    }

    $query = "SELECT level_id FROM cloudlevels";
    $result = mysqli_query($link, $query);
    
    /* numeric array */
    while($row = mysqli_fetch_array($result)) {
        $rows[] = $row["level_id"];
    }
    
    if(count($rows) != 0){
        $response["success"] = true;
        $response["levelIds"] = $rows;
    }else{
        $response["success"] = false;
    }

    /* free result set */
    mysqli_free_result($result);

    /* close connection */
    mysqli_close($link);
    
    echo json_encode($response);
?>