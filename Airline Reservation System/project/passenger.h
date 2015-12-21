#ifndef PASSENGER_H_INCLUDED
#define PASSENGER_H_INCLUDED

// constants for arrays size
#define MAXNAME 16               /* maximum lenght for any name(name, surname, country, city...)  */
#define MAXADDRESS 30            /* maximum lenght for address        */
#define MAXTITLE 6               /* maximum lenght for title          */
#define MAXPASSENGERS 50         /* maximum number of passengers      */
#define DATE 3                   /* number or array elements for date */


// constants for printing details in function get_name();
#define NAME 1                   /* number for name     */
#define SURNAME 2                /* number for surname  */
#define COUNTRY 3                /* number for country  */
#define CITY 4                   /* number for city     */
#define ADDRESS 5                /* number for address  */


// constants for printing details in function get_Int();
#define PASSDIGITS 9             /* how many digits a passport number must have  */
#define MOBDIGITS 11             /* how many digits a mobile number must have    */


/**************************************************************************************************************************/

/***************************************************************
 *   Sturcture for passenger informations
 ***************************************************************/
static struct Passenger
{
    char name[MAXNAME];                         // Passenger's name
    char surname[MAXNAME];                      // Passenger's surname
    char title[MAXTITLE];                       // Passenger's title
    char country[MAXNAME];                      // Passenger's country
    char city[MAXNAME];                         // Passenger's city
    char homeAddress[MAXADDRESS];               // Passenger's home address
    short DOB[DATE];                            // Passenger's date of birth
    short reservations[MAXPASSENGERS];          // Link to flights regarding reservations
    unsigned int passportNo;                    // Passenger's passpoert number
    unsigned long long int mobNo;               // Passenger's mobile number

}passenger[MAXPASSENGERS];
/**************************************************************************************************************************/







/**********************************************************************************************************************************************************/

/**************************************************************
 *   Functions prototypes
 **************************************************************/

unsigned long long int get_int(short n);                                                // function to get integer for any purpose
short get_input(short nMIN, short nMAX);                                                // function to get input from user
void  get_Title(char arr[]);                                                            // function to choose title(Mr.,Dr., ...)
void  get_date(short arr[], short minYear, short maxYear, short n);                     // function to get date in format dd/mm/yyyy
void  get_Name(char arr[], short n, short maxN);                                        // function to get name( of passenger, country, city, address...)
void  normalize_name(char arr[]);                                                       // function to normalize a name. e.g. lAZaR -> Lazar


short createNewPassenger(struct Passenger *passenger, short n);                         // function to create a new passenger
void  modifyPassenger(struct Passenger *passenger);                                     // function to modify an existing passenger
short listPassengers(const struct Passenger *passenger);                                // function to list all passengers and return their number
void  show_existingPassengers(const struct Passenger *passenger);                       // function to show all existing passenger's informations
void show_PassengerInfo(short n, const struct Passenger *passenger);                    // function to show a particular passenger's informations
short openPassengerFile(char *mode);                                                    // function for file opening
unsigned int checkPass(short chosenP, short count, const struct Passenger *passenger);  // function to check whther a passport number exist or not



/**********************************************************************************************************************************************************/
#endif
