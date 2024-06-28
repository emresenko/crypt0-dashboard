import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/crypto_provider.dart';
import '../models/crypto_pair.dart';
import 'details_screen.dart';
import '../widgets/line_chart_widget.dart'; 

class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  DateTime _startDate = DateTime(2020, 1, 1);
  DateTime _endDate = DateTime(2020, 2, 2);

  @override
  void initState() {
    super.initState();
    Provider.of<CryptoProvider>(context, listen: false).fetchDefaultCryptoPairs();
  }

  @override
  Widget build(BuildContext context) {
    final cryptoProvider = Provider.of<CryptoProvider>(context);

    return Scaffold(
      appBar: AppBar(
        title: Text('Crypto Dashboard'),
      ),
      body: Column(
        children: [
          Row(
            children: [
              Text('Start Date:'),
              SizedBox(width: 10),
              Text(_startDate.toIso8601String().split('T').first),
              ElevatedButton(
                onPressed: () async {
                  final selectedDate = await showDatePicker(
                    context: context,
                    initialDate: _startDate,
                    firstDate: DateTime(2000),
                    lastDate: DateTime.now(),
                  );
                  if (selectedDate != null) {
                    setState(() {
                      _startDate = selectedDate;
                    });
                  }
                },
                child: Text('Select Start Date'),
              ),
            ],
          ),
          Row(
            children: [
              Text('End Date:'),
              SizedBox(width: 10),
              Text(_endDate.toIso8601String().split('T').first),
              ElevatedButton(
                onPressed: () async {
                  final selectedDate = await showDatePicker(
                    context: context,
                    initialDate: _endDate,
                    firstDate: DateTime(2000),
                    lastDate: DateTime.now(),
                  );
                  if (selectedDate != null) {
                    setState(() {
                      _endDate = selectedDate;
                    });
                  }
                },
                child: Text('Select End Date'),
              ),
            ],
          ),
          ElevatedButton(
            onPressed: () {
              cryptoProvider.fetchCryptoPairs(_startDate, _endDate);
            },
            child: Text('Fetch Data'),
          ),
          if (cryptoProvider.isLoading)
            CircularProgressIndicator()
          else
            Expanded(
              child: ListView.builder(
                itemCount: cryptoProvider.cryptoPairs.length,
                itemBuilder: (context, index) {
                  final pair = cryptoProvider.cryptoPairs[index];
                  return Column(
                    children: [
                      Text(pair.pairName),
                      Container(
                        height: 200,
                        child: LineChartWidget(cryptoPairs: [pair]),
                      ),
                      ListTile(
                        title: Text(pair.pairName),
                        subtitle: Text('Date: ${pair.date.toIso8601String().split('T').first}'),
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => DetailsScreen(pair: pair),
                            ),
                          );
                        },
                      ),
                    ],
                  );
                },
              ),
            ),
        ],
      ),
    );
  }
}
