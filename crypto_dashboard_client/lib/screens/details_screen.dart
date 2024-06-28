import 'package:flutter/material.dart';
import '../models/crypto_pair.dart';

class DetailsScreen extends StatelessWidget {
  final CryptoPair pair;

  DetailsScreen({required this.pair});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(pair.pairName),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Date: ${pair.date.toIso8601String().split('T').first}'),
            Text('Open: ${pair.open}'),
            Text('Close: ${pair.close}'),
            Text('High: ${pair.high}'),
            Text('Low: ${pair.low}'),
            Text('Price: ${pair.price}'),
            Text('Volume: ${pair.volume}'),
            Text('Change Percentage: ${pair.changePercentage}%'),
          ],
        ),
      ),
    );
  }
}
