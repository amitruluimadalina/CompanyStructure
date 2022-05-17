import React from "react";
import { Card } from "react-bootstrap";
import styles from "./CardStyles.module.scss";
const CardComponent = (props) => {
    return(
    <Card border="dark" className={styles.card}>
        <Card.Body>
            <Card.Title>{props.title}</Card.Title>
        </Card.Body>
    </Card>
    )
};
export default CardComponent;