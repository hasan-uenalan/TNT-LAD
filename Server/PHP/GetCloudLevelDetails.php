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
    
    $level_id = $_POST["levelId"];

    $query = "SELECT level_name, preview_image, level_content FROM cloudlevels WHERE level_id=?";
    $stmt = mysqli_prepare($link, $query);
    mysqli_stmt_bind_param($stmt, "i", $level_id);
    
    if(mysqli_stmt_execute($stmt)){
        mysqli_stmt_store_result($stmt);
        mysqli_stmt_bind_result($stmt, $level_name, $preview_image, $level_content);
        
        $response["success"] = true;
        while(mysqli_stmt_fetch($stmt)){
            $response["levelName"] = $level_name;
            $response["previewImage"] = $preview_image;
            $response["levelContent"] = $level_content;
        }

    }else{
        $response["success"] = false;   
    }

    /* close connection */
    mysqli_close($link);
    
    echo json_encode($response);
?>