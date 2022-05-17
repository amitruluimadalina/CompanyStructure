import React from "react";
import { useState, useEffect } from "react";
import { Container, Row, Col } from "react-bootstrap";
import styles from "./DepartmentViewStyles.module.scss";
import Navigation from "../components/Navigation";
import CardComponent from "../components/Card";
const DepartmentView = () => {
    const navLinks=[{linkName: 'Home', linkPath: '/'},{linkName: 'Employee', linkPath: '/employee'}];
    let [departmentData, setDepartmentData] = useState([]);
    const env = process.env.REACT_APP_API;
    function refreshList(){
        fetch(env + 'department')
        .then(response=> response.json())
        .then(data=>{
            setDepartmentData(data)
        })
    }
    useEffect(()=>{
        refreshList()
    })
    return (
    <div className={styles.geometric}>
        <Navigation links={navLinks} variant="light"/>
        <div className="mt-5 d-flex justify-content-center">
            <Container className="mw-70">
                <Row className="text-center py-5">
                    <h3 className="text-white font-weight-bold">Departments</h3>
                </Row>
                <Row >
                {departmentData.length !==0 && 
                    departmentData.map( el =>
                        <Col sm={12} md={6} lg={4} key={el.departmentId} className='d-flex justify-content-center'>
                            <CardComponent title={el.DepartmentName}/>
                        </Col>
                    )
                }
                </Row>
            </Container>
        </div>
    </div>)
   
};
export default DepartmentView;