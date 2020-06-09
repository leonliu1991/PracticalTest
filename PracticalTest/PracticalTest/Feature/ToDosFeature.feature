Feature: ToDos

#Background: 
#Given I have got the environment runsettings

@TodoAPI
Scenario Outline: Todo API - Positive flow 01 - Check if a list of all to-dos is returned successfully and contains 200 todos
	When I call GET a list of to-dos
	Then it should return <StatusCode> and a list of <Count> to-dos

Examples:
| StatusCode | Count |
| 200        | 200   |

@TodoAPI
Scenario Outline: Todos API - Positive flow 02 - Check if a single to-do is returned successfully
	When I call GET a to-do by <Id>
	Then it should return <StatusCode> and a single to-do with correct <Id>

Examples:
| Id | StatusCode | 
| 25 | 200        | 

@TodoAPI
Scenario Outline: Todos API - Positive flow 03 - Check if a to-do can be created successfully
	When I call POST a to-do with <Id>, <Title>, <Body>
	Then it should return <StatusCode> and a single to-do with <Id>, <Title>, <Body>

Examples:
| Id  | Title | Body | StatusCode |
| 201 | foo   | bar  | 201        |

@TodoAPI
Scenario Outline: Todos API - Positive flow 04 - Check if a to-do can be updated successfully
	When I call PUT a to-do with <Id>, <Title>, <Body>
	Then it should return <StatusCode> and a single to-do with <Id>, <Title>, <Body>

Examples:
| Id | Title | Body | StatusCode |
| 25 | foo   | bar  | 200        |

@TodoAPI
Scenario Outline: Todos API - Positive flow 05 - Check if a to-do can be deleted successfully
	When I call DELETE a to-do with <Id>
	Then it should return <StatusCode> and empty response

Examples:
| Id  | StatusCode |
| 25  | 200        |

@TodoAPI
Scenario Outline: Todos API - Negative flow 01 - 404 Not found error should be returned when the id is invalid or not existed
	When I call GET a to-do by <Id>
	Then it should return <StatusCode> and empty response

Examples:
| Id   | StatusCode |
| 2000 | 404        |
| ^&   | 404        |
| abc  | 404        |

