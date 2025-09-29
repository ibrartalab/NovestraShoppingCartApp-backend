Entity Relationship Diagram (ERD) Overview
The core relationships among these entities are:

User can have Many Orders (1:M).

User has One Cart (1:1).

Cart can have Many CartItems (1:M).

Order can have Many OrderItems (1:M).

Product can be in Many CartItems and Many OrderItems (1:M relationship with each intermediate entity).

Category can have Many Products (1:M).