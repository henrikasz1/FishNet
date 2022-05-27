import { View, Text, StyleSheet, useWindowDimensions, ScrollView, ActivityIndicator} from 'react-native';
import React, {useState, useEffect} from 'react';
import { Switch } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import CustomMsgBox from '../../components/CustomMessageBox';
import GoBackHeader from '../../components/GoBackHeader';
import Icon from 'react-native-vector-icons/dist/SimpleLineIcons';
import { BaseUrl } from '../../components/Common/BaseUrl'
import axios from 'axios';

const EditProfileScreen = ({ route }) => {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [defaultPhoto, setDefaultPhoto] = useState(false);
  const [password, setPassword] = useState('')
  const [passwordRepeat, setPasswordRepeat] = useState('')
  const [isProfilePrivate, setIsProfilePrivate] = useState(false)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [message, setMessage] = useState('')
  const [loading, setLoading] = useState(true);
  const [oldPassword, setOldPassword] = useState('');
  const [passwordCorrect, setPasswordCorrect] = useState(true);

  const getUserById = `${BaseUrl}/api/user/getbyid/${route.params.userId}/`;

  const {height} = useWindowDimensions();
  const navigation = useNavigation();

  const onRegisterPressed = () => {
      checkPasswords()
      setIsSubmitting(true)
      handleMessage('')
      if (email == '' || firstName == '' || lastName == '') {
          handleMessage('Please fill in all "Manage Profile" fields');
          setIsSubmitting(false);
      }
      else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email))
      {
         handleMessage('Invalid email address')
         setIsSubmitting(false)
      }
      else if (password !== '' || passwordRepeat !== '')
      {
          console.warn(oldPassword)
          if (!passwordCorrect)
          {
              handleMessage('Password incorrect')
              setIsSubmitting(false)
          }
          else if (password !== passwordRepeat)
          {
              handleMessage('Passwords do not match')
              setIsSubmitting(false)
          }
      }
      else {
          handleUpdate({firstName, lastName, email, isProfilePrivate});
          setIsSubmitting(false);
          navigation.push("MainScreen")
      }

  }

  const checkPasswords = () => {
    const url = `${BaseUrl}/api/user/checkpassword/${oldPassword}`;

    axios
        .get(url)
        .then((response) => {
            setPasswordCorrect(response.data)
        })
        .catch(err => {
            console.log(err);
        })
  }

  const handleUpdate = (credentials) => {
    const url = `${BaseUrl}/api/user/update/${route.params.userId}`;

    axios
        .put(url, credentials)
        .then((response) => {
            const result = response.data;
            const {token, success, errors} = result;
        })
        .catch(error => {
            setIsSubmitting(false)
            handleMessage(error.response.data.errors)
        })

    const url2 = `${BaseUrl}/api/user/changePassword/${password}`;
    if (passwordCorrect)
    {
        axios
            .put(url2)
            .then((response) => {
                const result = response.data;
                const {token, success, errors} = result;
            })
            .catch(error => {
                setIsSubmitting(false)
                handleMessage(error.response.data.errors)
            })
    }
  }

  const handleMessage = (message) => {
      setMessage(message);
  }

  useEffect(() => {
    const getData = async () => {

      await axios
        .get(getUserById)
        .then(response => {
            setFirstName(response.data.name);
            setLastName(response.data.lastName);
            setEmail(response.data.email);
            setLoading(false);
            setIsProfilePrivate(response.data.isProfilePrivate);
        })
        .catch(err => {
          console.log(err);
          setLoading(false)
        })
    }
    getData();

  }, [loading])


  return (
    <View style={styles.root}>

        <GoBackHeader
            onPressBack={() => navigation.pop()}
            text="Edit Profile"
        />
        {!loading ?
        <ScrollView style={styles.container}> 
            <View style={styles.first}>
                <Text style={styles.text}> Manage Profile </Text>
                <CustomInput
                    placeholder="First Name"
                    iconType="user"
                    value={firstName}
                    setValue={setFirstName}
                />

                <CustomInput
                    placeholder="Last Name"
                    iconType="user"
                    value={lastName}
                    setValue={setLastName}
                />
                
                <CustomInput
                    placeholder="Email address"
                    iconType="envelope"
                    value={email}
                    setValue={setEmail}
                />

                <View style={styles.private}>
                    <Switch
                        style={styles.toggle}
                        value={isProfilePrivate}
                        onValueChange={setIsProfilePrivate}
                    />
                    <Text> Make your profile private </Text>
                </View>

                <View style={styles.private}>
                    <Switch
                        style={styles.toggle}
                        value={defaultPhoto}
                        onValueChange={setDefaultPhoto}
                    />
                    <Text> Use default image as your main photo </Text>
                </View>
            </View>

            <View>
                <Text style={styles.text}> Change Password </Text>

                <CustomInput
                    placeholder="Old Password"
                    iconType="lock"
                    value={oldPassword}
                    setValue={setOldPassword}
                    secureTextEntry={true}
                />

                <CustomInput
                    placeholder="New Password"
                    iconType="lock"
                    value={password}
                    setValue={setPassword}
                    secureTextEntry={true}
                />

                
                <CustomInput
                    placeholder="Repeat New Password"
                    iconType="lock-open"
                    value={passwordRepeat}
                    setValue={setPasswordRepeat}
                    secureTextEntry={true}
                />
            </View>

            <CustomMsgBox text={message} />

            {!isSubmitting ?
                <CustomButton 
                text="Update"
                onPress={onRegisterPressed}
                type="primary"
            />
            :
            <CustomButton
                text={<ActivityIndicator size="small" color="white" />}>
            </CustomButton>
            }
        </ScrollView>
        :
        <View style={styles.activityIndicator}>
            <ActivityIndicator size="large" color="#3B71F3" />
        </View>}
    </View>
  )
}

const styles = StyleSheet.create({
    root: {
        alignItems: 'center',
        flex: 1,
    },
    button:{
        color: 'red'
    },
    first: {
        borderBottomWidth: 0.5,
        borderColor: 'gray',
        marginBottom: '4%'
    },
    container: {
        padding: 20,
    },
    header: {
        width: "100%",
        flex: 1,
        flexDirection: "row",
        marginBottom: 25,
        textAlign: "center"
    },
    text: {
        fontWeight: 'bold',
        marginBottom: '2%',
        fontSize: 15
    },
    icon: {
        marginTop: "1.5%",
        width: "20%",
    },
    title: {
        width: "85%",
        fontSize: 24,
        fontWeight: 'bold',
        color: '#051C60',
        textAlign: "left",
    },
    toggle: {
        transform: [{ scaleX: 1.6}, {scaleY: 1.6}],
        marginRight: "5%",
        marginBottom: "5%"
    },
    private: {
        marginRight: 'auto',
        paddingLeft: '3%',
        marginTop: '7%',
        flexDirection: 'row'
    },
    policy: {
        color: 'grey',
        padding: '2%'
    },
    link: {
        color: '#FD8075'
    },
    activityIndicator: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center'
    }
});

export default EditProfileScreen