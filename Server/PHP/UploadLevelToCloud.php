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
    
    $level_name = $_POST["levelName"];
    $preview_image = $_POST["previewImage"];
    $level_content = $_POST["levelContent"];

    $query = "INSERT INTO cloudlevels (level_name, preview_image, level_content) VALUES (?, ?, ?)";
    $stmt = mysqli_prepare($link, $query);
    mysqli_stmt_bind_param($stmt, "sss", $level_name, $preview_image, $level_content);
    
    if(mysqli_stmt_execute($stmt)){
        $response["success"] = true;
    }else{
        $response["success"] = false;   
    }

    /* close connection */
    mysqli_close($link);
    
    echo json_encode($response);
?>