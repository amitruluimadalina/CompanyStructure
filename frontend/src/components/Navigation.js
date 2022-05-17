import React from "react";
import { Navbar, Container, Nav } from "react-bootstrap";

const Navigation = (props) => {
    return (
        <Navbar variant={props.variant}>
        <Container>
        <Navbar.Brand>Company</Navbar.Brand>
        <Nav>
         {props.links && props.links.map(link=>
          <Nav.Link key={link.linkName} href={link.linkPath}>{link.linkName}</Nav.Link>
          )}
        </Nav>
        </Container>
      </Navbar>);
};
export default Navigation;